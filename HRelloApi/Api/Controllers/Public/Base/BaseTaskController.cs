using AutoMapper;
using Dal.Tasks.Entities;
using HRelloApi.Controllers.Public.Base.dto.Response;
using Logic.Managers.Tasks.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRelloApi.Controllers.Public.Base;

public class BaseTaskController : BasePublicController
{
    private readonly IMapper _mapper;
    private readonly ITaskUnitOfWorkManager _manager;

    /// <summary>
    /// Конструктор
    /// </summary>
    public BaseTaskController(IMapper mapper, ITaskUnitOfWorkManager manager)
    {
        _mapper = mapper;
        _manager = manager;
    }

    /// <summary>
    /// Получение задачи 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskById(Guid id)
    {
        var task = await _manager.GetAsync<TaskDal>(id);
        return Ok(new TaskResponse
        {
            Task = task
        });
    }

    /// <summary>
    /// Возвращает все задачи
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("all")]
    public IActionResult GetAllTasks()
    {
        var list = _manager.GetAll<TaskDal>();
        return Ok(new AllTasksResponse
        {
            Tasks = list
        });
    }
}