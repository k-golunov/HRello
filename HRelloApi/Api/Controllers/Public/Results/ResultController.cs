using AutoMapper;
using Dal.TaskResult.Entities;
using Dal.Tasks.Entities;
using HRelloApi.Controllers.Public.Base;
using HRelloApi.Controllers.Public.Results.dto.Request;
using HRelloApi.Controllers.Public.Results.dto.Response;
using Logic.Constants;
using Logic.Managers.Result.Interfaces;
using Logic.Managers.Tasks.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRelloApi.Controllers.Public.Results;

/// <summary>
/// Контроллер для итогов от главного руководителя
/// </summary>
public class ResultController : BasePublicController
{
    private readonly IResultManager _taskResultManager;
    private readonly ITaskUnitOfWorkManager _taskUnitOfWorkManager;
    private readonly IMapper _mapper;

    public ResultController(IResultManager taskResultManager, ITaskUnitOfWorkManager taskUnitOfWorkManager,
        IMapper mapper)
    {
        _mapper = mapper;
        _taskResultManager = taskResultManager;
        _taskUnitOfWorkManager = taskUnitOfWorkManager;
    }

    /// <summary>
    /// Получение итога по его айди
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetTaskResultResponse), 200)]
    [Authorize( AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetTaskResultsAsync([FromRoute] Guid id)
    {
        var taskResultDal = await _taskResultManager.GetAsync(id);
        var response = _mapper.Map<GetTaskResultResponse>(taskResultDal);
        
        return Ok(response);
    }
    
    /// <summary>
    /// Создание итога
    /// </summary>
    /// <param name="request"></param>
    /// <remarks>
    /// listId задач проходит валидацию на квартал и год, они должны совпадать во всех задачах
    /// </remarks>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IdResponse), 200)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleConstants.MainBoss)]
    public async Task<IActionResult> CreateTaskResultsAsync([FromBody] CreateTaskResultRequest request)
    {
        var taskResultDal = _mapper.Map<TaskResultDal>(request);
        var task = await _taskUnitOfWorkManager.GetByListIdAsync<TaskDal>(request.TasksId);
        _taskResultManager.ValidationData(task, request.Year, request.Quarter);
        taskResultDal.Tasks = task;
        var responseId = await _taskResultManager.InsertAsync(taskResultDal);
        
        return Ok(new IdResponse
        {
            Id = responseId
        });
    }
    
    /// <summary>
    /// Получение всех итогов со связанными данными
    /// </summary>
    /// <returns></returns>
    [HttpGet("all")]
    [ProducesResponseType(typeof(GetAllTaskResultResponse), 200)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetAllTaskResultsAsync()
    {
        var allTaskResults = await _taskResultManager.GetAllAsync();
        
        return Ok(new GetAllTaskResultResponse
        {
            AllTaskResultResponse = allTaskResults.Select(_mapper.Map<GetTaskResultResponse>).ToList()
        });
    }
}