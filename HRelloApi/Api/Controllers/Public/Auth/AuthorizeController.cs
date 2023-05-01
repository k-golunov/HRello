using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Dal.Entities;
using HRelloApi.Controllers.Public.Auth.Dto.Request;
using HRelloApi.Controllers.Public.Auth.Dto.Response;
using HRelloApi.Controllers.Public.Base;
using HRelloApi.Notification;
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
        IOptions<JWTSettings> options,
        IMapper mapper)
    {
        LogContext.PushProperty("Source", "Test Authorize Controller");
        _userManager = userManager;
        _signInManager = signInManager;
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
    ///
    /// !ПОКА ЧТО ВОЗВРАЩАЕТ ID СОЗДАННОГО ПОЛЬЗОВАТЕЛЯ
    /// </returns>
    //[Authorize(Roles = "boss")]
    [HttpPost("createUser")]
    [ProducesResponseType(typeof(Guid), 200)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserModelRequest model)
    {
        var userByEmail = await _userManager.FindByEmailAsync(model.Email);
        if (userByEmail is not null)
            return Conflict();
        
        var user = _mapper.Map<UserDal>(model);
        
        var result = await _userManager.CreateAsync(user);

        var roleResult = await _userManager.AddToRoleAsync(user, model.Role);
        
        if (result.Succeeded && roleResult.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            
            var claims = new List<Claim>();
            claims.Add(new Claim("Email", model.Email));
            claims.Add(new Claim("DepartmentId", model.DepartamentId.ToString()));
            claims.Add(new Claim("Role", model.Role));

            await _userManager.AddClaimsAsync(user, claims);
        }
        else
        {
            return BadRequest();
        }
        EmailSender.SendEmail("Success", "kostya.golunov2015@yandex.ru");
        //var userDal = await _userManager.FindByEmailAsync(user.Email);
        return Ok(user.Id);
    }

    private string GetToken(UserDal user)
    {
        var claims = new List<Claim> { new ("Id", user.Id) };
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
    /// Регистрация пользователя
    /// не выдает токены, только заполняет данные в бд
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("register/{userId:guid}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Register([FromRoute] Guid userId, [FromBody] RegisterModelRequest model)
    {
        var unregisteredUser = await _userManager.FindByIdAsync(userId.ToString());
        var user = _mapper.Map(model, unregisteredUser);
        user.EmailConfirmed = true;
        //var passwordUpdateResult = await _userManager.UpdatePasswordAsync(user, model.Password);
        var passwordUpdateResult = await _userManager.AddPasswordAsync(user, model.Password);
        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded && passwordUpdateResult.Succeeded)
        {
            return Ok();
        }

        return BadRequest();

    }

    /// <summary>
    /// Авторизация пользователя в системе
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <returns>access и refresh токены</returns>
    [HttpPost("signin")]
    [ProducesResponseType(typeof(TokenResponse), 200)]
    public async Task<IActionResult> SignIn(SignInModelRequest model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        
        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

        if (result.Succeeded)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var token = GetToken(user);

            return Ok(new TokenResponse
            {
                AccessToken = token,
                RefreshToken = "нет реализации)))"
            });
        }

        return Unauthorized();
    }

    [HttpPost("token")]
    [ProducesResponseType(200)]
    public IActionResult RefreshToken()
    {
        //_signInManager.()
        
        return Ok();
    }
}