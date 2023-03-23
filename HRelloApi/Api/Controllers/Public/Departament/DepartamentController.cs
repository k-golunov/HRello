using AutoMapper;
using Dal.Entities;
using HRelloApi.Controllers.Public.Base;
using HRelloApi.Controllers.Public.Departament.Dto.Request;
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
    public async Task<IActionResult> CreateDepartament(CreateDepartamentRequest request)
    {
        // TODO проверка, что BossId существует
        var dal = _mapper.Map<DepartamentDal>(request);
        var response = await _manager.InsertAsync(dal);
        return Ok(response);
    }
}