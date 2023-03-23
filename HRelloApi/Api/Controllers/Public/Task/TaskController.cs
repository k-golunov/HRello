using System.Security.Claims;
using AutoMapper;
using Dal.Entities;
using Dal.Tasks.Entities;
using Dal.Tasks.Enum;
using HRelloApi.Controllers.Public.Base;
using HRelloApi.Controllers.Public.Task.dto.request;
using Logic.Managers.Task.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRelloApi.Controllers.Public.Task;

/// <summary>
/// Контроллер для рестов связанных с задачами
/// </summary>
public class TaskController: BasePublicController
{
    private readonly ITaskStatusManager _statusManager;
    private readonly ITaskManager _taskManager;
    private readonly UserManager<UserDal> _userManager;
    private readonly IMapper _mapper;

    /// <summary>
    /// Конструктор
    /// </summary>
    public TaskController(ITaskStatusManager statusManager, 
        UserManager<UserDal> userManager, 
        ITaskManager taskManager,
        IMapper mapper)
    {
        _statusManager = statusManager;
        _userManager = userManager;
        _taskManager = taskManager;
        _mapper = mapper;
    }

    /// <summary>
    /// Рест для создания задачи
    /// </summary>
    [HttpPost("create")]
    public async Task<IActionResult> CreateTask(CreateTaskRequest model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId.ToString());
        if (user == null)
            return BadRequest();
        var task = new TaskDal(user);
        var response = await _taskManager.InsertAsync(task);
        return Ok(response); //вообще по идеи здесь можно сделать redirect на страницу с редактированием
    }

    /// <summary>
    /// Рест для редактирования данных о задаче
    /// </summary>
    [HttpPut("edit/task={id:guid}")]
    public async Task<IActionResult> EditTask([FromRoute] Guid taskId, EditTaskRequest model)
    {
        var oldTask = await _taskManager.GetAsync(taskId);
        if (oldTask == null)
            return NotFound();
        var task = _mapper.Map(model, oldTask);
        task.Status += 1;
        return Ok();//здесь тоже можно редирект на страницу со всеми задачами
    }
    
    
}