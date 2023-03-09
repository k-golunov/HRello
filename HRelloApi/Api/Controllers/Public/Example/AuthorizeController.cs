using System.Security.Claims;
using Dal.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Context;

namespace HRelloApi.Controllers.Public.Example;

[ApiController]
[Route("api/v1/public/[controller]")]
public class AuthorizeController : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<UserDal> _userManager;
    private readonly JWTSettings _options;

    public AuthorizeController(UserManager<UserDal> userManager)
    {
        LogContext.PushProperty("Source", "Test Authorize Controller");
        _userManager = userManager;
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
    public async Task<IActionResult> Register()
    {
        /*var user = new UserDal();
        await _signInManager.SignInAsync(user, isPersistent: false);*/

        // создание пользователя
        /*var a = await _userManager.CreateAsync(new UserDal()
        {
            Email = "asc@mail.ru",
            UserName = "ass",
            PhoneNumber = "79221234820",
            DepartamentId = 1,
            Name = "Vasya"
        });*/

        // получение пользователя по айди
        Log.Information("получение пользователя по id");
        var b = await _userManager.FindByIdAsync("ff9a04b4-697c-42c0-92ce-2debdeadc059");
        return Ok(b);
    }
}