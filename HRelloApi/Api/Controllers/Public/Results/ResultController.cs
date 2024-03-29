﻿using AutoMapper;
using Dal.TaskResult.Entities;
using Dal.Tasks.Entities;
using HRelloApi.Attributes;
using HRelloApi.Controllers.Public.Base;
using HRelloApi.Controllers.Public.Results.dto.Request;
using HRelloApi.Controllers.Public.Results.dto.Response;
using Logic.Constants;
using Logic.Excel;
using Logic.Managers.Result.Interfaces;
using Logic.Managers.Tasks.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

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
    /*[CustomAuthorize(Roles = RoleConstants.MainBoss)]*/
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
    [ProducesResponseType(typeof(List<BlockTaskResultResponse>), 200)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetAllTaskResultsAsync()
    {
        var allTaskResults = await _taskResultManager.GetAllGroupedByBlocksAsync();
        
        return Ok(allTaskResults.Select(x => new BlockTaskResultResponse()
        {
            BlockId = x.Key,
            TaskResults = x.Value.Select(_mapper.Map<GetTaskResultResponse>).ToList()
        }));
    }

    /// <summary>
    /// рест для редактирования итога
    /// </summary>
    [HttpPut]
    [ProducesResponseType(typeof(IdResponse), 200)]
    [CustomAuthorize(Roles = RoleConstants.MainBoss)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> EditTaskResultAsync([FromBody] EditTaskResultRequest request)
    {
        var taskResult = await _taskResultManager.GetAsync(request.Id);
        var newResult = _mapper.Map(request, taskResult);
        var id = await _taskResultManager.UpdateAsync(newResult);
        return Ok(new IdResponse { Id = id });
    }

    /// <summary>
    /// рест для удаления итога
    /// </summary>
    [HttpDelete]
    [CustomAuthorize(Roles = RoleConstants.MainBoss)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DeleteTaskResultAsync([FromQuery] Guid id)
    {
        await _taskResultManager.DeleteAsync(id);
        return Ok();
    }

    [HttpGet("download")]
    [CustomAuthorize(Roles = RoleConstants.MainBoss)]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> DownloadTaskResultAsync([FromQuery] TaskResultFilters filters)
    {
        var excel = await _taskResultManager.GenerateFileAsync(filters.Year, filters.Quarters, filters.DepartmentsId);
        return File(excel, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"Итоги за {filters.Year} год {string.Join(", ", filters.Quarters)} квартал(ы)");
    }
}