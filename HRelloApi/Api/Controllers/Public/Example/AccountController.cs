using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HRelloApi.Controllers.Public.Example;

/// <summary>
/// Контроллер для выдачи токена авторизованным пользователям
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AccountController: Controller
{
    /// <summary>
    /// параметры токена из appsettings
    /// </summary>
    private readonly JWTSettings _options;

    public AccountController(IOptions<JWTSettings> options)
    {
        _options = options.Value;
    }

    /// <summary>
    /// выдает токен содержащий вводную информацию о пользователе (имя, роль или еще что-то)
    /// </summary>
    /// <returns>jwt-token</returns>
    [HttpGet("getToken")]
    public string GetToken(/* сюда добавить сущность,ссодержащую инфу, передающуюся в токен*/)
    {
        List<Claim> claims = new List<Claim>();
        //закидываем в лист переданную инфу

        var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

        var jwt = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(signInKey, SecurityAlgorithms.Sha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);

    }
}