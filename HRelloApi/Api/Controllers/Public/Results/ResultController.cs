using AutoMapper;
using Dal.TaskResult.Entities;
using HRelloApi.Controllers.Public.Base;
using HRelloApi.Controllers.Public.Results.dto.Request;
using HRelloApi.Controllers.Public.Results.dto.Response;
using Logic.Managers.Result.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRelloApi.Controllers.Public.Results;

/// <summary>
/// Контроллер для итогов от главного руководителя
/// </summary>
public class ResultController : BasePublicController
{
    private readonly ITaskResultManager _taskResultManager;
    private readonly IMapper _mapper;

    public ResultController(ITaskResultManager taskResultManager, IMapper mapper)
    {
        _mapper = mapper;
        _taskResultManager = taskResultManager;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetTaskResultResponse), 200)]
    public async Task<IActionResult> GetTaskResultsAsync([FromRoute] Guid id)
    {
        var taskResultDal = await _taskResultManager.GetAsync(id);
        var response = _mapper.Map<GetTaskResultResponse>(taskResultDal);
        
        return Ok(response);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(IdResponse), 200)]
    public async Task<IActionResult> CreateTaskResultsAsync([FromBody] CreateTaskResultRequest request)
    {
        //TODO валидация
        
        var taskResultDal = _mapper.Map<TaskResultDal>(request);
        var responseId = await _taskResultManager.InsertAsync(taskResultDal);
        
        return Ok(new IdResponse
        {
            Id = responseId
        });
    }
}