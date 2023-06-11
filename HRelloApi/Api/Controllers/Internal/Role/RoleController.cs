using Dal.Entities;
using HRelloApi.Controllers.Base.Exception;
using HRelloApi.Controllers.Internal.Role.Dto.Request;
using HRelloApi.Controllers.Internal.Role.Dto.Response;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRelloApi.Controllers.Internal.Role;

/// <summary>
/// 
/// </summary>
[Route("api/v1/public/role")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<UserDal> _userManager;

    public RoleController(RoleManager<IdentityRole> roleManager, UserManager<UserDal> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    /// <summary>
    /// Создание роли
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
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
        return Ok(new AllRolesResponse
        {
            Roles = roles
        });
    }
    
    /// <summary>
    /// Получение роли пользователя
    /// </summary>
    /// <returns>все роли</returns>
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserRole([FromRoute] Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            return NotFound(new BaseExceptionModel("User.404", "User not found"));
        }
        var role = await _userManager.GetRolesAsync(user);
        return Ok(new GetUserRoleResponse()
        {
            Role = role[0]
        });
    }
}