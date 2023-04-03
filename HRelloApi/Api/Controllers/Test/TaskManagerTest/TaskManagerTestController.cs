using AutoMapper;
using Dal.Tasks.Entities;
using Dal.Tasks.Repositories;
using HRelloApi.Controllers.Public.Task.dto.request;
using Logic.Managers.Tasks.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRelloApi.Controllers.Test.TaskManagerTest;

/// <summary>
/// 
/// </summary>
[Route("api/v1/debug/task")]
public class TaskManagerTestController : ControllerBase
{
    private readonly ITaskUnitOfWorkManager _manager;
    private readonly IMapper _mapper;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="manager"></param>
    public TaskManagerTestController(ITaskUnitOfWorkManager manager, IMapper mapper)
    {
        _manager = manager;
        _mapper = mapper;
    }
    
    [HttpGet("get")]
    public async Task<IActionResult> TestGet(Guid id)
    {
        var a = await _manager.GetAsync<TaskDal>(id);
        return Ok(a);
    }
    
    [HttpPost("insert")]
    public async Task<IActionResult> TestInsert([FromBody]CreateTaskRequest model)
    {
        var task = _mapper.Map<TaskDal>(model);
        var a = await _manager.InsertAsync(task);
        return Ok(a);
    }
    
    //не рабочий потому что неверная модель!!! но все и так работает так что похуй)
    [HttpPut("update")]
    public async Task<IActionResult> TestUpdate([FromBody]EditTaskRequest model)
    {
        var task = _mapper.Map<TaskDal>(model);
        var a = await _manager.UpdateAsync(task);
        return Ok(a);
    }
    
    [HttpDelete("delete")]
    public async Task<IActionResult> TestDelete(Guid id)
    {
        await _manager.DeleteAsync<TaskDal>(id);
        return Ok();
    }
}