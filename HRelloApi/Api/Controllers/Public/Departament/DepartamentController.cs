using System.Net;
using AutoMapper;
using Dal.Entities;
using HRelloApi.Controllers.Base.Exception;
using HRelloApi.Controllers.Public.Base;
using HRelloApi.Controllers.Public.Departament.Dto.Request;
using HRelloApi.Controllers.Public.Departament.Dto.Response;
using Logic.Exceptions.Base;
using Logic.Managers.Departament.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRelloApi.Controllers.Public.Departament;

/// <summary>
/// Контроллер для работы с отделами
/// </summary>
public class DepartamentController : BasePublicController
{
    private readonly IDepartamentManager _manager;
    private readonly IMapper _mapper;

    public DepartamentController(IDepartamentManager manager, IMapper mapper)
    {
        _mapper = mapper;
        _manager = manager;
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
        // TODO проверка, что BossId существует
        var dal = _mapper.Map<DepartamentDal>(request);
        var response = new CreateIdResponse
        {
            Id = await _manager.InsertAsync(dal)
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
        var departament = await _manager.GetAsync(request.DepartamentId);
        if (departament == null)
            return NotFound(new BaseExceptionModel("Department.404", "Отдел не найден"));
        departament.BossId = request.BossId;
        await _manager.UpdateAsync(departament);
        return Ok();
    }

        /// <summary>
    /// Получение всех отделов
    /// </summary>
    /// <returns>все отделы</returns>
    [HttpGet("all")]
    public IActionResult GetAllDepartament()
    {
        var a = _manager.GetAll();
        return Ok(a);
    }
}