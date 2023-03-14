using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRelloApi.Controllers.Debug.AuthTest;

/// <summary>
/// контроллер для тестирования авторизации
/// </summary>
[Microsoft.AspNetCore.Components.Route("api/v1/debug/auth")]
public class AuthTestController : ControllerBase
{
    /// <summary>
    /// Рест для тестирования jwt middleware
    /// </summary>
    [HttpGet("test")]
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult Test()
    {
        return Ok("Is it work!");
    }
}