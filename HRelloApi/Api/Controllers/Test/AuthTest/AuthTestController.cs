using Logic.Managers.Departament.Interfaces;
using Logic.Managers.Identity.Helpers;
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
    private readonly IdentityHelper _identityHelper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public AuthTestController(IDepartamentManager departamentManager, IdentityHelper identityHelper, IHttpContextAccessor httpContextAccessor)
    {
        _departamentManager = departamentManager;
        _identityHelper = identityHelper;
        _httpContextAccessor = httpContextAccessor;
    }
    
    /// <summary>
    /// Рест для тестирования 
    /// </summary>
    [HttpGet("test")]
    public IActionResult Test()
    {
        var userId = _httpContextAccessor.HttpContext.User.Identity.Name;
        var id = _identityHelper.GetUserId();
        return Ok(userId);
    }
}