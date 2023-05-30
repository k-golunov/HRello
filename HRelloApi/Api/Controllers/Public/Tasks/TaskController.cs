using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Dal.Entities;
using Dal.Tasks.Entities;
using Dal.Tasks.Enum;
using HRelloApi.Controllers.Public.Base;
using HRelloApi.Controllers.Public.Tasks.dto.request;
using HRelloApi.Controllers.Public.Tasks.dto.response;
using Logic.Exceptions.Tasks;
using Logic.Exceptions.User;
using Logic.Managers.Tasks.Filters;
using Logic.Managers.Tasks.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using Serilog.Core;

namespace HRelloApi.Controllers.Public.Tasks;

/// <summary>
/// Контроллер для рестов связанных с задачами
/// </summary>
public class TaskController: BasePublicController
{
    /// <summary>
    /// мэнэджер для работы с пользователями
    /// </summary>
    private readonly UserManager<UserDal> _userManager;
    /// <summary>
    /// маппер
    /// </summary>
    private readonly IMapper _mapper;
    /// <summary>
    /// основной мэнэджер для работы со всеми сущностями связанных с задачами
    /// </summary>
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
    [ProducesResponseType(typeof(TaskIdResponse), 200)]
    public async Task<IActionResult> CreateTask(CreateTaskRequest model)
    {
        var task = _mapper.Map<CreateTaskRequest,TaskDal>(model);
        var token = Request.Headers["Authorization"].ToString().Split(' ')[1];
        var result = await _manager.CreateTaskAsync(task, model.BlockId, token);
        var response =new TaskIdResponse { Id = result };
        return Ok(response);
    }

    /// <summary>
    /// Редактирования данных о задаче
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPut]
    [ProducesResponseType(typeof(TaskIdResponse), 200)]
    public async Task<IActionResult> EditTask(EditTaskRequest model)
    {
        var oldTask = await _manager.GetAsync<TaskDal>(model.Id);
        if (oldTask == null)
            throw new TaskNotFoundException(model.Id);
        var task = _mapper.Map(model, oldTask);
        var token = Request.Headers["Authorization"].ToString().Split(' ')[1];
        var response = new TaskIdResponse { Id = await _manager.UpdateTaskAsync(task, model.BlockId, token, model.Comment) };
        return Ok(response);
    }

    /// <summary>
    /// Рест на изменение статуса задачи
    /// </summary>
    [HttpPatch("change-status")]
    public async Task<IActionResult> ChangeStatus(ChangeStatusRequest model)
    {
        if (model.NextStatus == StatusEnum.CompletionCheck || model.NextStatus == StatusEnum.Completed)
            throw new WrongUrlForChangeStatusException("/api/v1/public/task/review или  /api/v1/public/task/complete");
        await _manager.ChangeStatus(model.Id, model.NextStatus, model.Comment);
        return Ok();
    }

    /// <summary>
    /// рест на завершение задачи сотрудником
    /// </summary>
    [HttpPost("review")]
    [ProducesResponseType(typeof(TaskResultResponse), 200)]
    public async Task<IActionResult> CheckCompletion(UserTaskCompletedRequest model)
    {
        var task = await _manager.GetAsync<TaskDal>(model.TaskId);
        if (task?.Status != StatusEnum.InWork)
            throw new WrongUrlForChangeStatusException(
                "/api/v1/public/task/change-status или /api/v1/public/task/complete");
        var userResult = _mapper.Map<UserTaskResultDal>(model);
        var result = await _manager.SendResultForTask(userResult, task, StatusEnum.CompletionCheck);
        return Ok(new TaskResultResponse { ResultId = result, TaskId = task.Id });

    }

    /// <summary>
    /// рест на полное завершение задачи
    /// </summary>
    [HttpPost("complete")]
    [ProducesResponseType(typeof(TaskResultResponse), 200)]
    public async Task<IActionResult> CompleteTask(BossTaskCompletedRequest model)
    {
        var task = await _manager.GetAsync<TaskDal>(model.TaskId);
        if (task == null)
            throw new TaskNotFoundException(model.TaskId);
        if (task?.Status != StatusEnum.CompletionCheck)
            throw new WrongUrlForChangeStatusException(
                "/api/v1/public/task/change-status или /api/v1/public/task/review");
        var bossResult = _mapper.Map<BossTaskResultDal>(model);
        var id = await _manager.SendResultForTask(bossResult, task, StatusEnum.Completed, model.Comment);
        return Ok(new TaskResultResponse { ResultId = id, TaskId = task.Id });

    }
    
    /// <summary>
    /// рест на получение конкретной задачи
    /// </summary>
    [HttpGet("{taskId:guid}")]
    [ProducesResponseType(typeof(TaskResponse), 200)]
    public async Task<IActionResult> GetTask([FromRoute] Guid taskId)
    {
        var task = await _manager.GetAsync<TaskDal>(taskId);
        var userResult = await _manager.GetAllAsync<UserTaskResultDal>();
        var bossResult = await _manager.GetAllAsync<BossTaskResultDal>();
        if (task == null)
            throw new TaskNotFoundException(taskId);
        var taskResponse = _mapper.Map<TaskDal, TaskResponse>(task);
        taskResponse.UserResult = _mapper.Map<UserResultResponse>(userResult.FirstOrDefault(x => x.TaskId == taskId));
        taskResponse.BossResult = _mapper.Map<BossResultResponse>(bossResult.FirstOrDefault(x => x.TaskId == taskId));
        return Ok(taskResponse);
    }

    /// <summary>
    /// рест на получение задач по заданным фильтрам и по страницам по 10 штук
    /// </summary>
    [HttpGet("all/{page:int?}")]
    [ProducesResponseType(typeof(AllTasksResponse), 200)]
    public async Task<IActionResult> GetAllTasks([FromRoute] int page,[FromQuery] FiltersRequest filtersRequest)
    {
        var filters = _mapper.Map<Filters>(filtersRequest);
        var tasksDals = await _manager.GetAllAsync<TaskDal>();
        var filteredTasks = _manager.ApplyFilters(filters, tasksDals);
        var tasks = filteredTasks.Select(_mapper.Map<TaskResponse>).ToList();
        tasks = tasks.Skip(10 * (page - 1)).Take(10).ToList();
        return Ok(new AllTasksResponse(tasks.Count, filteredTasks.Count / 10 + 1, tasks));
    }

    /// <summary>
    /// Проверяет наличие переданного блока задач в БД
    /// При успешно найденном блоке присваивает ее задаче
    /// </summary>
    /// <param name="task">задача</param>
    /// <param name="blockId">id блока задач</param>
    /// <exception cref="BlockNotFoundException">ошибка при не найденном блоке</exception>
    [NonAction]
    private async Task SetBlockForTask(TaskDal task, Guid blockId)
    {
        var block = await _manager.GetAsync<BlockDal>(blockId);
        if (block == null)
            throw new BlockNotFoundException(blockId);
        task.Block = block;
    }
}