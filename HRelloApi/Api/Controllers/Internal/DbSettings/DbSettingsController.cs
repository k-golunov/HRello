using Dal.Entities;
using Logic.Constants;
using Logic.Managers.Departament.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Cms;

namespace HRelloApi.Controllers.Internal.DbSettings;

/// <summary>
/// Контроллер для заполенения бд дефолтными данными
/// и отчистки бд
/// </summary>
[ApiController]
[Route("api/v1/internal/db-settings")]
public class DbSettingsController : ControllerBase
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IDepartamentManager _departamentManager;
    
    public DbSettingsController(RoleManager<IdentityRole> roleManager, IDepartamentManager departamentManager)
    {
        _departamentManager = departamentManager;
        _roleManager = roleManager;
    }

    /// <summary>
    /// Добавление дефолтных ролей
    /// </summary>
    /// <returns></returns>
    [HttpPost("set-default-role")]
    public async Task<IActionResult> SetDefaultRoleAsync()
    {
        var roleBoss = new IdentityRole(RoleConstants.Boss);
        var roleEmployee = new IdentityRole(RoleConstants.Employee);
        var roleMainBoss = new IdentityRole(RoleConstants.MainBoss);
        await _roleManager.CreateAsync(roleBoss);
        await _roleManager.CreateAsync(roleEmployee);
        await _roleManager.CreateAsync(roleMainBoss);
        return Ok();
    }

    /// <summary>
    /// Заполнение дефолтных отделов
    /// Требует предварительно создания всех руководителей
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> SetDefaultDepartamentAsync()
    {
        var departament1 = new DepartamentDal
        {
            Id = 0,
            Name = "",
            BossId = "1"
        };
        await _departamentManager.InsertAsync(departament1);
        return Ok();
    }
}