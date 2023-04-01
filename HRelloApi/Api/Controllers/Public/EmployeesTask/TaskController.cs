using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Dal.Entities;
using Dal.Tasks.Entities;
using Dal.Tasks.Enum;
using HRelloApi.Controllers.Public.Base;
using HRelloApi.Controllers.Public.Task.dto.request;
using Logic.Managers.Tasks.Interfaces;
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
    private readonly ITaskUnitOfWorkManager _manager;

    /// <summary>
    /// Конструктор
    /// </summary>
    public TaskController(ITaskStatusManager statusManager, 
        UserManager<UserDal> userManager, 
        ITaskManager taskManager,
        IMapper mapper, ITaskUnitOfWorkManager manager)
    {
        _statusManager = statusManager;
        _userManager = userManager;
        _taskManager = taskManager;
        _mapper = mapper;
        _manager = manager;
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
        var auth = Request.Headers["Authorization"].ToString().Split(' ')[1];
        var jwt = handler.ReadToken(auth) as JwtSecurityToken;
        var email = jwt.Claims.First(x => x.Type == ClaimTypes.Email).Value;
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return BadRequest();
        task.User = user;
        //var response = await _taskManager.InsertAsync(task);
        var response = await _manager.CreateTaskAsync(task);
        return Ok(response);
    }

    /// <summary>
    /// Рест для редактирования данных о задаче
    /// </summary>
    #if !DEBUG
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    #endif
    [HttpPut("edit/task={taskId:guid}")]
    public async Task<IActionResult> EditTask([FromRoute] Guid taskId, EditTaskRequest model)
    {
        var oldTask = await _taskManager.GetAsync(taskId);
        if (oldTask == null)
            return NotFound();
        var task = _mapper.Map(model, oldTask);
        try
        {
            _statusManager.ChangeStatus(task, StatusEnum.OnChecking);
        }
        catch
        {
            BadRequest();
        }
        var response = await _manager.UpdateTaskAsync(task);
        return Ok(response);
    }
}