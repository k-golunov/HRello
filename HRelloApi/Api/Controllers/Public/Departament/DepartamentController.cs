using System.Net;
using AutoMapper;
using Dal.Entities;
using Dal.User.Models;
using HRelloApi.Controllers.Base.Exception;
using HRelloApi.Controllers.Public.Base;
using HRelloApi.Controllers.Public.Departament.Dto.Request;
using HRelloApi.Controllers.Public.Departament.Dto.Response;
using Logic.Exceptions.Base;
using Logic.Exceptions.Department;
using Logic.Exceptions.User;
using Logic.Managers.Departament.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRelloApi.Controllers.Public.Departament;

/// <summary>
/// Контроллер для работы с отделами
/// </summary>
public class DepartamentController : BasePublicController
{
    private readonly IDepartamentManager _departamentManager;
    private readonly UserManager<UserDal> _userManager;
    private readonly IMapper _mapper;

    /// <summary>
    /// Конструтор
    /// </summary>
    public DepartamentController(IDepartamentManager departamentManager, UserManager<UserDal> userManager, IMapper mapper)
    {
        _mapper = mapper;
        _userManager = userManager;
        _departamentManager = departamentManager;
    }
    
    /// <summary>
    /// Создание отдела
    /// </summary>
    /// <param name="request">входная модель для создания</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateDepartamentAsync(CreateDepartamentRequest request)
    {
        var dal = _mapper.Map<DepartamentDal>(request);
        var response = new CreateIdResponse
        {
            Id = await _departamentManager.InsertAsync(dal)
        };
        return Ok(response);
    }

    /// <summary>
    /// Прикрепление руководителя к отделу
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPatch("bossId")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> UpdateBossIdAsync(SetBossIdRequest request)
    {
        var departament = await _departamentManager.GetAsync(request.DepartamentId);
        if (departament == null)
            throw new DepartmentNotFoundException(request.DepartamentId);
        var boss = await _userManager.FindByIdAsync(request.BossId);
        if (boss == null)
            throw new UserNotFoundException(request.BossId);
        var roles = await _userManager.GetRolesAsync(boss);
        if (!roles.Contains("boss") && !roles.Contains("mainboss"))
            throw new BadRoleForDepartmentBossException();
        departament.BossId = request.BossId;
        await _departamentManager.UpdateAsync(departament);
        return Ok();
    }

    /// <summary>
    /// Получение всех отделов
    /// </summary>
    /// <returns>все отделы</returns>
    [HttpGet("all")]
    public async Task<IActionResult> GetAllDepartment()
    {
        var a = await _departamentManager.GetAllAsync();
        return Ok(a);
    }
    
    /// <summary>
    /// Создание отдела
    /// </summary>
    /// <param name="request">входная модель для создания</param>
    /// <returns></returns>
    [HttpPost("with-boss-id")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateDepartmentWithBossIdAsync(CreateDepartmentWithBossIdRequest request)
    {
        var dal = _mapper.Map<DepartamentDal>(request);
        var response = new CreateIdResponse
        {
            Id = await _departamentManager.InsertAsync(dal)
        };
        return Ok(response);
    }
    
    /// <summary>
    /// Обновление отдела пользователя
    /// </summary>
    /// <param name="request">входная модель для создания</param>
    /// <returns></returns>
    [HttpPatch("user-department")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateUsersDepartment(UpdateUserDepartmentRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            return NotFound(new BaseExceptionModel("User.404", "User not found"));
        }
        user.DepartamentId = request.NewDepartmentId;
        await _userManager.UpdateAsync(user);

        return Ok();
    }
}