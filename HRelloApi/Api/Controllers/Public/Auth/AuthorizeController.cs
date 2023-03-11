using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Dal.Entities;
using HRelloApi.Controllers.Public.Auth.Dto.Request;
using HRelloApi.Controllers.Public.Auth.Dto.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Context;

namespace HRelloApi.Controllers.Public.Auth;

/// <summary>
/// Контроллер, принимающий запросы на запросы, связанные с авторизацией пользователей
/// </summary>
[ApiController]
[Route("api/v1/public/[controller]")]
public class AuthorizeController : ControllerBase
{
    private readonly SignInManager<UserDal> _signInManager;
    private readonly Logic.Managers.UserManager<UserDal> _userManager;
    private readonly JWTSettings _options;
    private readonly IMapper _mapper;

    /// <summary>
    /// Конструктор контроллера
    /// </summary>
    /// <param name="userManager">Сервис логики работы с пользователями</param>
    /// <param name="signInManager">Сервис логики работы с аутентификацией пользователей</param>
    /// <param name="options">Настройки уникального токена</param>
    /// <param name="mapper">Автомаппер</param>
    public AuthorizeController(Logic.Managers.UserManager<UserDal> userManager, 
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
    [HttpPost("createUser")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserModelRequest model)
    {
        var user = _mapper.Map<UserDal>(model);
        
        var result = await _userManager.CreateAsync(user);
        
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);

            var claims = new List<Claim>();
            claims.Add(new Claim("Email", model.Email));
            claims.Add(new Claim("DepartmentId", model.DepartamentId.ToString()));
            claims.Add(new Claim("Role", model.Role));
            claims.Add(new Claim("UserName", model.UserName));

            await _userManager.AddClaimsAsync(user, claims);
        }
        else
        {
            BadRequest();
        }

        var userDal = await _userManager.FindByEmailAsync(user.Email);
        return Ok(new IdModelResponse(Guid.Parse(user.Id)));
        
        
        // создание пользователя
        var a = await _userManager.CreateAsync(new UserDal()
        {
            Email = "asc@mail.ru",
            UserName = "ass",
            PhoneNumber = "79221234820",
            DepartamentId = 1,
            Name = "Vasya"
        });

        // получение пользователя по айди
        Log.Information("получение пользователя по id");
        var b = await _userManager.FindByIdAsync("ff9a04b4-697c-42c0-92ce-2debdeadc059");
        return Ok(b);
    }

    private string GetToken(UserDal user, IEnumerable<Claim> principal)
    {
        var claims = principal.ToList();
        
        claims.Add(new Claim(ClaimTypes.Name, user.Name));
        
        var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

        var jwt = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256Signature));
        

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromQuery] Guid userId, [FromBody] RegisterModelRequest model)
    {
        var unregisteredUser = await _userManager.FindByIdAsync(userId.ToString());
        var user = _mapper.Map(model, unregisteredUser);
        var passwordUpdateResult = await _userManager.UpdatePasswordAsync(user, model.Password);

        var result = await _userManager.UpdateAsync(user);

        if (result.Succeeded && passwordUpdateResult.Succeeded)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }

    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn(SignInModelRequest model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        
        var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

        if (result.Succeeded)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var token = GetToken(user, claims);

            return Ok(token);
        }
        else
        {
            return Unauthorized();
        }
    }
}