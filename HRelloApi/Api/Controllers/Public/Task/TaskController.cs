using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Dal.Entities;
using Dal.Tasks.Entities;
using Dal.Tasks.Enum;
using HRelloApi.Controllers.Public.Base;
using HRelloApi.Controllers.Public.Task.dto.request;
using Logic.Managers.Task.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost("create")]
    public async Task<IActionResult> CreateTask(CreateTaskRequest model)
    {
        var task = _mapper.Map<TaskDal>(model);
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadToken(Request.Headers["Authorization"].ToArray()[1]);
        //todo!
        var user = await _userManager.FindByIdAsync(model.UserId.ToString());
        if (user == null)
            return BadRequest();
        task.User = user;
        var response = await _taskManager.InsertAsync(task);
        return Ok(response);
    }

    /// <summary>
    /// Рест для редактирования данных о задаче
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut("edit/task={taskId:guid}")]
    public async Task<IActionResult> EditTask([FromRoute] Guid taskId, EditTaskRequest model)
    {
        var oldTask = await _taskManager.GetAsync(taskId);
        if (oldTask == null)
            return NotFound();
        var task = _mapper.Map(model, oldTask);
        task.Status += 1;
        var response = await _taskManager.UpdateAsync(task);
        return Ok(response);
    }
    
    
}