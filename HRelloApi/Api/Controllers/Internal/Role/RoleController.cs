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
    public async Task<IActionResult> CreateUserRole(CreateRoleInternalRequest request)
    {
        var a = await _roleManager.CreateAsync(request.RoleName);
        return Ok(a.Succeeded);
    }
}