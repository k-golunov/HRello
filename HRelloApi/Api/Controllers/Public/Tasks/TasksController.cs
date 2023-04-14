using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Dal.Entities;
using Dal.Tasks.Entities;
using Dal.Tasks.Enum;
using HRelloApi.Controllers.Public.Base;
using HRelloApi.Controllers.Public.EmployeeTask.dto.response;
using HRelloApi.Controllers.Public.Task.dto.request;
using HRelloApi.Controllers.Public.Tasks.dto.request;
using HRelloApi.Controllers.Public.Tasks.dto.response;
using Logic.Managers.Tasks.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRelloApi.Controllers.Public.Tasks;

/// <summary>
/// Контроллер для рестов связанных с задачами
/// </summary>
public class TasksController: BasePublicController
{
    //private readonly ITaskStatusManager _statusManager;
    private readonly UserManager<UserDal> _userManager;
    private readonly IMapper _mapper;
    private readonly ITaskUnitOfWorkManager _manager;

    /// <summary>
    /// Конструктор
    /// </summary>
    public TasksController(UserManager<UserDal> userManager,
        IMapper mapper, ITaskUnitOfWorkManager manager)
    {
        _userManager = userManager;
        _mapper = mapper;
        _manager = manager;
    }

    /// <summary>
    /// Рест для создания задачи
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<IActionResult> CreateTask(CreateTaskRequest model)
    {
        var task = _mapper.Map<TaskDal>(model);
        var handler = new JwtSecurityTokenHandler();
        var auth = Request.Headers["Authorization"].ToString().Split(' ')[1];
        var jwt = handler.ReadToken(auth) as JwtSecurityToken;
        var userId = jwt.Claims.First(x => x.Type == "Id").Value;
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return BadRequest();
        task.User = user;
        //var response = await _taskManager.InsertAsync(task);
        var response =new TaskIdResponse { Id = await _manager.CreateTaskAsync(task) };
        return Ok(response);
    }

    /// <summary>
    /// Редактирования данных о задаче
    /// </summary>
    #if !DEBUG
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    #endif
    [HttpPut("{taskId:guid}")]
    public async Task<IActionResult> EditTask([FromRoute] Guid taskId, EditTaskRequest model)
    {
        var oldTask = await _manager.GetAsync<TaskDal>(taskId);
        if (oldTask == null)
            return NotFound();
        var task = _mapper.Map(model, oldTask);
       var result = await _manager.IsChangeStatus(task, StatusEnum.OnChecking);
       if (!result)
           return BadRequest(); 
        var response = new TaskIdResponse {Id = await _manager.UpdateTaskAsync(task) };
        return Ok(response);
    }

    /// <summary>
    /// Рест на изменение статуса задачи
    /// </summary>
    /// todo! сделать добавление в историю и комментарий
    [HttpPatch("change")]
    public async Task<IActionResult> ChangeStatus(ChangeStatusRequest model)
    {
        if (model.NextStatus == StatusEnum.CompletionCheck || model.NextStatus == StatusEnum.Completed)
            return BadRequest();//неправильный рест
        var task = await _manager.GetAsync<TaskDal>(model.Id);
        if (task == null)
            return NotFound();//задача не найдена
        var result = await _manager.IsChangeStatus(task, model.NextStatus);
        if (!result)
            return BadRequest();//невозможно изменить статус
        return Ok();
    }

    /// <summary>
    /// рест на завершение задачи сотрудником
    /// </summary>
    [HttpPost("review")]
    public async Task<IActionResult> CheckCompletion(UserTaskCompletedRequest model)
    {
        var task = await _manager.GetAsync<TaskDal>(model.TaskId);
        if (task?.Status == StatusEnum.InWork)
        {
            var userResult = _mapper.Map<UserTaskResultDal>(model);
            var result = await _manager.InsertAsync(userResult);
            await _manager.IsChangeStatus(task, StatusEnum.CompletionCheck);
            return Ok(new TaskResultResponse { ResultId = result, TaskId = task.Id });
        }

        return BadRequest();//невозможно заврешить задачу с текущим статусом
    }

    /// <summary>
    /// рест на полное завершение задачи
    /// </summary>
    [HttpPost("complete")]
    public async Task<IActionResult> CompleteTask(BossTaskCompletedRequest model)
    {
        var task = await _manager.GetAsync<TaskDal>(model.TaskId);
        if (task?.Status != StatusEnum.CompletionCheck)
        {
            var bossResult = _mapper.Map<BossTaskResultDal>(model);
            var id =await _manager.InsertAsync(bossResult);
            await _manager.IsChangeStatus(task, StatusEnum.Completed);
            return Ok(new TaskResultResponse { ResultId = id, TaskId = task.Id });
        }

        return BadRequest();
    }
    
    /// <summary>
    /// рест на получение конкретной задачи
    /// </summary>
    [HttpGet("{taskId:guid}")]
    public async Task<IActionResult> GetTask([FromRoute] Guid taskId)
    {
        var task = await _manager.GetAsync<TaskDal>(taskId);
        if (task == null)
            return NotFound();
        var taskResponse = _mapper.Map<TaskResponse>(task);
        return Ok(taskResponse);
    }
    
    //рест на получение всех задач с фильтрами
}