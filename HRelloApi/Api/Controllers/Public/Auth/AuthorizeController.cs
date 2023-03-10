using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dal.Entities;
using HRelloApi.Controllers.Public.Example.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Context;

namespace HRelloApi.Controllers.Public.Example;

[ApiController]
[Route("api/v1/public/[controller]")]
public class AuthorizeController : ControllerBase
{
    private readonly SignInManager<UserDal> _signInManager;
    private readonly UserManager<UserDal> _userManager;
    private readonly JWTSettings _options;

    public AuthorizeController(UserManager<UserDal> userManager, SignInManager<UserDal> signInManager, IOptions<JWTSettings> options)
    {
        LogContext.PushProperty("Source", "Test Authorize Controller");
        _userManager = userManager;
        _signInManager = signInManager;
        _options = options.Value;
    }

    //todo!
    /// <summary>
    /// Тестовый рест
    /// в данный момент в нем показано, как добавлять новых пользователей через UserManager
    /// без пароля и каких-то еще данных, главное указать почту, username и departamentId
    /// также показано, как получить пользователя
    /// </summary>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register(Request request)
    {
        var user = new UserDal();
        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);

            var claims = new List<Claim>();
            claims.Add(new Claim("Email", request.Email));
            claims.Add(new Claim("Password", request.Password));

            await _userManager.AddClaimsAsync(user, claims);
        }
        else
        {
            BadRequest();
        }

        return Ok();
        
        
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

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn(Request request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

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