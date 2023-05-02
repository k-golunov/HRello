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
public class TaskController: BasePublicController
{
    //private readonly ITaskStatusManager _statusManager;
    private readonly UserManager<UserDal> _userManager;
    private readonly IMapper _mapper;
    private readonly ITaskUnitOfWorkManager _manager;

    /// <summary>
    /// Конструктор
    /// </summary>
    public TaskController(UserManager<UserDal> userManager,
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
        var user = await GetUserFromTokenAsync();
        if (user == null)
            return BadRequest();
        task.User = user;
        var result = await _manager.CreateTaskAsync(task);
        task.Id = result;
        await _manager.CreateNewHistoryEntry(task, ActionTypeEnum.OnChecking, "Создание задачи");
        var response =new TaskIdResponse { Id = result};
        return Ok(response);
    }

    /// <summary>
    /// Редактирования данных о задаче
    /// </summary>
    #if !DEBUG
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    #endif
    [HttpPut("")]
    public async Task<IActionResult> EditTask(EditTaskRequest model)
    {
        var oldTask = await _manager.GetAsync<TaskDal>(model.Id);
        if (oldTask == null)
            return NotFound();
        var task = _mapper.Map(model, oldTask);
        var action = await _manager.GetActionFromChangeStatus(task, StatusEnum.OnChecking);
        if (action == ActionTypeEnum.None)
           return BadRequest(); 
        var response = new TaskIdResponse {Id = await _manager.UpdateTaskAsync(task) };
        await _manager.CreateNewHistoryEntry(task, action, "Доработка задачи");
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
        var action = await _manager.GetActionFromChangeStatus(task, model.NextStatus);
        if (action == ActionTypeEnum.None)
            return BadRequest();//невозможно изменить статус
        await _manager.CreateNewHistoryEntry(task, action, model.Comment);
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
            var action = await _manager.GetActionFromChangeStatus(task, StatusEnum.CompletionCheck);
            await _manager.CreateNewHistoryEntry(task, action, model.Result);
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
        if (task != null && task?.Status == StatusEnum.CompletionCheck)
        {
            var bossResult = _mapper.Map<BossTaskResultDal>(model);
            var id =await _manager.InsertAsync(bossResult);
            var action = await _manager.GetActionFromChangeStatus(task, StatusEnum.Completed);
            await _manager.CreateNewHistoryEntry(task, action, model.Comment);
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

    [NonAction]
    private async Task<UserDal> GetUserFromTokenAsync()
    {
        var handler = new JwtSecurityTokenHandler();
        var auth = Request.Headers["Authorization"].ToString().Split(' ')[1];
        var jwt = handler.ReadToken(auth) as JwtSecurityToken;
        var userId = jwt.Claims.First(x => x.Type == "Id").Value;
        var user = await _userManager.FindByIdAsync(userId);
        return user;
    }

    /// <summary>
    /// рест на получение задач по заданным фильтрам и по страницам по 10 штук
    /// </summary>
    [HttpGet("all/{page:int?}")]
    [ProducesResponseType(typeof(AllTasksResponse), 200)]
    public async Task<IActionResult> GetAllTasks([FromRoute] int page,[FromQuery] FiltersRequest filters)
    {
        var tasks = _manager.GetAll<TaskDal>();
        foreach (var filter in filters.GetType().GetProperties())
        {
            var value = filter.GetValue(filters);
            if (value != null)
                tasks = _manager.ApplyFilter(tasks, filter.Name, value.ToString().Split(", "));
        }

        var count = tasks.Count;
        tasks = tasks.Skip(10 * (page - 1)).Take(10).ToList();
        return Ok(new AllTasksResponse(count, tasks));
    }
}