﻿using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Dal.Entities;
using HRelloApi.Attributes;
using HRelloApi.Controllers.Base.Exception;
using HRelloApi.Controllers.Public.Auth.Dto.Request;
using HRelloApi.Controllers.Public.Auth.Dto.Response;
using HRelloApi.Controllers.Public.Base;
using HRelloApi.Notification;
using Logic.Constants;
using Logic.Exceptions.Department;
using Logic.Exceptions.User;
using Logic.Managers.Departament.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Context;
using Serilog.Core;

namespace HRelloApi.Controllers.Public.Auth;

/// <summary>
/// Контроллер, принимающий запросы на запросы, связанные с авторизацией пользователей
/// </summary>
public class AuthorizeController : BasePublicController
{
    private readonly SignInManager<UserDal> _signInManager;
    private readonly UserManager<UserDal> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IDepartamentManager _departamentManager;
    private readonly JWTSettings _options;
    private readonly IMapper _mapper;

    /// <summary>
    /// Конструктор контроллера
    /// </summary>
    /// <param name="userManager">Сервис логики работы с пользователями</param>
    /// <param name="signInManager">Сервис логики работы с аутентификацией пользователей</param>
    /// <param name="options">Настройки уникального токена</param>
    /// <param name="mapper">Автомаппер</param>
    public AuthorizeController(UserManager<UserDal> userManager, 
        SignInManager<UserDal> signInManager,
        IDepartamentManager departamentManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<JWTSettings> options,
        IMapper mapper)
    {
        LogContext.PushProperty("Source", "Test Authorize Controller");
        _userManager = userManager;
        _signInManager = signInManager;
        _departamentManager = departamentManager;
        _roleManager = roleManager;
        _options = options.Value;
        _mapper = mapper;
    }
    
    /// <summary>
    /// Тестовый рест
    /// в данный момент в нем показано, как добавлять новых пользователей через UserManager
    /// без пароля и каких-то еще данных, главное указать почту, username и departamentId
    /// также показано, как получить пользователя
    ///
    /// Создает нового пользователя
    /// </summary>
    /// <returns>
    /// При успешном создании пользователя отправляет электронное письмо на почту созданного пользователя
    /// для дальнейшей его регистрации на сервисе
    /// </returns>
    /*[CustomAuthorize(Roles = RoleConstants.Boss)]*/
    [HttpPost("createUser")]
    [ProducesResponseType(typeof(Guid), 200)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserModelRequest model)
    {
        var userByEmail = await _userManager.FindByEmailAsync(model.Email);
        if (userByEmail is not null)
            return Conflict();
        
        var user = _mapper.Map<UserDal>(model);
        await SetDepartmentAndRoleForUser(user, model);
        
        var result = await _userManager.CreateAsync(user);
        await _userManager.AddToRoleAsync(user, model.Role);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            
            var claims = new List<Claim>();
            claims.Add(new Claim("Email", model.Email));
            claims.Add(new Claim("DepartmentId", model.DepartamentId.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, model.Role));

            await _userManager.AddClaimsAsync(user, claims);
        }
        else
            return BadRequest();
        
        EmailSender.SendEmail($"You can register by link: http://185.133.40.145:3000/registration/{user.Id}", model.Email);
        return Ok(new IdModelResponse
        {
            UserId = user.Id
        });
    }

    private async Task<string> GetToken(UserDal user, IEnumerable<Claim> principal)
    {
        var claims = principal.ToList();
        claims.Add(new Claim(ClaimTypes.Email, user.Email));
        claims.Add(new Claim("userId", user.Id));
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
           claims.Add(new Claim(ClaimTypes.Role, role)); 
        }
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretKey));
        var token = new JwtSecurityToken
        (
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddDays(30),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            
        );

        return tokenHandler.WriteToken(token);
    }

    /// <summary>
    /// регистраиция на сервисе
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("register")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Register([FromQuery] string userId, [FromBody] RegisterModelRequest model)
    {
        var unregisteredUser = await _userManager.FindByIdAsync(userId);
        if (unregisteredUser == null)
            throw new UserNotFoundException(userId);
        var user = _mapper.Map(model, unregisteredUser);
        var passwordUpdateResult = await _userManager.AddPasswordAsync(user, model.Password);
        user.EmailConfirmed = true;
        var result = await _userManager.UpdateAsync(user);

        return result.Succeeded && passwordUpdateResult.Succeeded ?
                Ok():
                BadRequest();
    }

    /// <summary>
    /// Вход на сервис и получение токенов пока что через это рест
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("signin")]
    [ProducesResponseType(typeof(TokenResponse), 200)]
    public async Task<IActionResult> SignIn(SignInModelRequest model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user is null)
            throw new UserNotFoundException(model.Email);
        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

        if (result.Succeeded)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var token = GetToken(user, claims);

            return Ok(new TokenResponse
            {
                AccessToken = await token,
                RefreshToken = "Нет реализации)))",
                UserId = user.Id
            });
        }

        return Unauthorized();
    }

    /// <summary>
    /// Проверка, есть ли инвайт у пользователя
    /// </summary>
    /// <param name="userId">идентификатор пользователя</param>
    /// <returns></returns>
    [HttpGet("check-invite/{userId}")]
    public async Task<IActionResult> CheckInvite([FromRoute] string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            throw new UserNotFoundException(userId);
        
        if (!user.EmailConfirmed)
            return Ok(new CheckInviteResponse
            {
                IsInvite = true
            });
        
        return Ok(new CheckInviteResponse
        {
            IsInvite = false
        });

    }

    /// <summary>
    /// проверяет корректность переданной роли и id отдела
    /// при успешной проверке присваивает их пользователю
    /// </summary>
    /// <param name="user">пользователь</param>
    /// <param name="request">входные данные</param>
    /// <exception cref="DepartmentNotFoundException">ошибка при неверном id отдела</exception>
    /// <exception cref="UserRoleNotFoundException">ошибка при неверно указанной роли пользователя</exception>
    [NonAction]
    private async System.Threading.Tasks.Task SetDepartmentAndRoleForUser(UserDal user, CreateUserModelRequest request)
    {
        var department = await _departamentManager.GetAsync(request.DepartamentId);
        if (department == null)
            throw new DepartmentNotFoundException(request.DepartamentId);
        var role = await _roleManager.FindByNameAsync(request.Role);
        if (role == null)
            throw new UserRoleNotFoundException(request.Role);
        user.Departament = department;
    }
}