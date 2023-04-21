using Logic.Managers.Departament.Interfaces;
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
    private readonly IDepartamentManager _departamentManager;
    public AuthTestController(IDepartamentManager departamentManager)
    {
        _departamentManager = departamentManager;
    }
    
    /// <summary>
    /// Рест для тестирования 
    /// </summary>
    [HttpGet("test")]
    public IActionResult Test()
    {
        _departamentManager.Test();
        return Ok("Is it work!");
    }
}