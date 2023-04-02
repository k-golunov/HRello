using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRelloApi.Controllers.Test;

/// <summary>
/// контроллер для тестирования авторизации
/// </summary>
[Route("api/v1/debug/auth")]
public class AuthTestController : ControllerBase
{
    /// <summary>
    /// Рест для тестирования jwt middleware
    /// </summary>
    [HttpGet("test")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult Test()
    {
        return Ok("Is it work!");
    }
}