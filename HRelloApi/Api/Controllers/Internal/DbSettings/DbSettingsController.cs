using Dal.Entities;
using Dal.TaskResult.Entities;
using Dal.Tasks.Entities;
using Dal.Tasks.Repositories.Interfaces;
using Dal.User.Models;
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
    private readonly IBlockRepository _blockRepository;

    public DbSettingsController(RoleManager<IdentityRole> roleManager, IDepartamentManager departamentManager,
        IBlockRepository blockRepository)
    {
        _departamentManager = departamentManager;
        _roleManager = roleManager;
        _blockRepository = blockRepository;
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
    /// </summary>
    /// <returns></returns>
    [HttpPost("set-default-department")]
    public async Task<IActionResult> SetDefaultDepartmentAsync()
    {
        var department1 = new DepartamentDal
        {
            Name = DepartmentConstants.Development
        };
        await _departamentManager.InsertAsync(department1);
        var department2 = new DepartamentDal
        {
            Name = DepartmentConstants.Study
        };
        await _departamentManager.InsertAsync(department2);
        var department3 = new DepartamentDal
        {
            Name = DepartmentConstants.CompensationBenefits
        };
        await _departamentManager.InsertAsync(department3);
        var department4 = new DepartamentDal
        {
            Name = DepartmentConstants.IntegratorsManufacturers
        };
        await _departamentManager.InsertAsync(department4);
        return Ok();
    }

    /// <summary>
    /// Создание дефолтных блоков в бд
    /// </summary>
    /// <returns></returns>
    [ProducesResponseType(200)]
    [HttpPost("set-default-block")]
    public async Task<IActionResult> SetDefaultBlockAsync()
    {
        await _blockRepository.InsertAsync(new BlockDal
        {
            Value = BlockConstants.Adaptation
        });
        await _blockRepository.InsertAsync(new BlockDal
        {
            Value = BlockConstants.Estimation
        });
        await _blockRepository.InsertAsync(new BlockDal
        {
            Value = BlockConstants.Selection
        });
        await _blockRepository.InsertAsync(new BlockDal
        {
            Value = BlockConstants.CorporateCulture
        });
        await _blockRepository.InsertAsync(new BlockDal
        {
            Value = BlockConstants.InternalWork
        });
        await _blockRepository.InsertAsync(new BlockDal
        {
            Value = BlockConstants.StaffDevelopment
        });
        await _blockRepository.InsertAsync(new BlockDal
        {
            Value = BlockConstants.HRSupport
        });
        await _blockRepository.InsertAsync(new BlockDal
        {
            Value = BlockConstants.HRBrandExternal
        });
        await _blockRepository.InsertAsync(new BlockDal
        {
            Value = BlockConstants.PersonnelAccountingAndSalary
        });
        
        return Ok(await _blockRepository.GetAllAsync());
    }
}