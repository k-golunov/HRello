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
    
    [HttpPost("test")]
    public async Task<IActionResult> Test(Guid id)
    {
        var a = await _manager.GetAsync<HistoryRepository>(typeof(HistoryDal), id) as HistoryDal;
        return Ok(a.Comment);
    }
}