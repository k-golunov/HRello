using HRelloApi.Controllers.Internal.Role.Dto.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRelloApi.Controllers.Internal.Role;

/// <summary>
/// 
/// </summary>
[Route("api/v1/internal/role")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    [HttpPost]
    [ProducesResponseType(200)]
    public async Task<IActionResult> CreateUserRole(CreateRoleInternalRequest request)
    {
        var a = await _roleManager.CreateAsync(request.RoleName);
        return Ok(a.Succeeded);
    }

    /// <summary>
    /// Получение всех ролей
    /// </summary>
    /// <returns>все роли</returns>
    [HttpGet("all")]
    public IActionResult GetAllRole()
    {
        var roles = _roleManager.Roles.ToList();
        return Ok(roles);
    }
}