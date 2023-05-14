using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using Dal;
using Dal.Base;
using Dal.Base.Entitities;
using Dal.Base.Interfaces;
using Dal.Entities;
using Dal.Tasks.Entities;
using Dal.Tasks.Enum;
using Dal.Tasks.Repositories;
using Dal.Tasks.Repositories.Interfaces;
using Logic.Exceptions.Tasks;
using Logic.Exceptions.User;
using Logic.Managers.Tasks.Interfaces;
using Logic.Managers.Tasks.StatusesTree;
using Microsoft.AspNetCore.Identity;

namespace Logic.Managers.Tasks;

/// <summary>
/// Реализация паттерна UnitOfWork для task и всех связанных сущностей, кроме юзера
/// Возможно еще и юзера
/// Возможно сюда включать также статус мененджер или репозиторий
/// </summary>
public class TaskUnitOfWorkManager : ITaskUnitOfWorkManager
{
    /// <summary>
    /// репозиторий задач
    /// </summary>
    private readonly ITaskRepository _taskRepository;
    /// <summary>
    /// реапозиторий историй задач
    /// </summary>
    private readonly IHistoryRepository _historyRepository;
    /// <summary>
    /// репозиторий итогов руководителя
    /// </summary>
    private readonly IBossTaskResultsRepository _bossTaskResultsRepository;
    /// <summary>
    /// репозиторий итогов сотрудника
    /// </summary>
    private readonly IUserTaskResultsRepository _userTaskResultsRepository;
    /// <summary>
    /// репозиторий блоков задач
    /// </summary>
    private readonly IBlockRepository _blockRepository;

    private readonly UserManager<UserDal> _userManager;

    /// <summary>
    /// Конструтор
    /// </summary>
    public TaskUnitOfWorkManager(
        ITaskRepository taskRepository, 
        IHistoryRepository historyRepository, 
        IBossTaskResultsRepository bossTaskResultsRepository, 
        IUserTaskResultsRepository userTaskResultsRepository,
        IBlockRepository blockRepository,
        UserManager<UserDal> userManager)
    {
        _taskRepository = taskRepository;
        _historyRepository = historyRepository;
        _userTaskResultsRepository = userTaskResultsRepository;
        _bossTaskResultsRepository = bossTaskResultsRepository;
        _blockRepository = blockRepository;
        _userManager = userManager;
    }
    
    /// <summary>
    /// Операция создания новой задачи
    /// </summary>
    /// <param name="taskDal">сущность задачи с новыми данными</param>
    /// <param name="blockId">id обновленного блока задачи</param>
    /// <param name="token">jwt пользователя</param>
    /// <returns>id созданной записи</returns>
    public async Task<Guid> CreateTaskAsync(TaskDal taskDal, Guid blockId, string token)
    {
        await CheckAndSetDataForTask(taskDal, token, blockId);
        taskDal.Id = await _taskRepository.InsertAsync(taskDal);
        await CreateNewHistoryEntry(taskDal, ActionTypeEnum.OnChecking);
        return taskDal.Id;
    }

    /// <summary>
    /// обновляет данные о задаче
    /// </summary>
    /// <param name="taskDal">сущность задачи с новыми данными</param>
    /// <param name="blockId">id обновленного блока задачи</param>
    /// <param name="token">jwt пользователя</param>
    /// <returns>id обновленной записи</returns>
    public async Task<Guid> UpdateTaskAsync(TaskDal taskDal, Guid blockId, string token)
    {
        await CheckAndSetDataForTask(taskDal, token, blockId);
        var taskId = await _taskRepository.UpdateAsync(taskDal);
        var action = await ChangeStatusAndGetAction(taskDal, StatusEnum.OnChecking);
        await CreateNewHistoryEntry(taskDal, action);
        return taskId;
    }

    /// <summary>
    /// Возвращает список задач, соотвествующих входящим фильтрам
    /// </summary>
    public List<TaskDal> ApplyFilters(Filters.Filters filters, List<TaskDal> tasks)
    {
        foreach (var filter in filters.GetType().GetProperties())
        {
            var value = filter.GetValue(filters);
            if (value != null)
                tasks = ApplyFilter(tasks, filter.Name, value.ToString().Split(", "));
        }

        return tasks;
    }

    /// <summary>
    /// Возвращает список задач, соответсвующих одному входящему фильтру
    /// </summary>
    private List<TaskDal> ApplyFilter(List<TaskDal> tasks, string field, string[] filters)
    {
        return tasks
            .Where(x => 
                filters.Contains(typeof(TaskDal)
                    .GetProperty(field)?
                    .GetValue(x)?
                    .ToString()))
            .ToList();
    }

    public async Task<Guid> ChangeStatus(Guid taskId, StatusEnum nextStatus, string comment)
    {
        var task = await _taskRepository.GetAsync(taskId);
        if (task == null)
            throw new TaskNotFoundException(taskId);
        var action = await ChangeStatusAndGetAction(task, nextStatus);
        await CreateNewHistoryEntry(task, action, comment);
        return task.Id;
    }

    public async Task<Guid> SendResultForTask<T>(T taskResult, TaskDal task, StatusEnum status, string comment = null) where T: BaseDal<Guid>
    {
        var result = await InsertAsync(taskResult);
        var action = await ChangeStatusAndGetAction(task, status);
        await CreateNewHistoryEntry(task, action, comment);
        return result;
    }

    /// <summary>
    /// Проверяет возможность смены статуса задачи на входящий
    /// Изменяет статус задачи при корректно заданном следующем статусе
    /// Возвращет действие - изменение статуса задачи - для записи истории изменения задачи
    /// </summary>
    private async Task<ActionTypeEnum> ChangeStatusAndGetAction(TaskDal task, StatusEnum nextStatus)
    {
        var action = StatusesGraph.StatusesGraph.GetAction(task.Status, nextStatus);
        if (action == ActionTypeEnum.None)
            throw new StatusChangeException(task.Status.ToString(), nextStatus.ToString());
        task.Status = nextStatus;
        await _taskRepository.UpdateAsync(task);
        return action;
    }

    /// <summary>
    /// возвращает передаваемую в дженерик параметре сущность с входным id
    /// </summary>
    public async Task<T?> GetAsync<T>(Guid id) where T : BaseDal<Guid>
    {
        var repository = GetRepository<T>();
        var result = await repository.GetAsync(id);
        return result;
    }

    /// <summary>
    /// обновляет передаваемую в дженерик параметре сущность новой моделью
    /// </summary>
    /// <param name="dal">новая модель данных</param>
    public async Task<Guid> UpdateAsync<T>(T dal) where T : BaseDal<Guid>
    {
        var repository = GetRepository<T>();
        var id = await repository.UpdateAsync(dal);
        return id;
    }

    /// <summary>
    /// добавляет новую сущность
    /// </summary>
    /// <param name="dal">модель создаваемой сущности</param>
    /// <typeparam name="T">тип создаваемой сущнсти</typeparam>
    /// <returns>id новой записи</returns>
    public async Task<Guid> InsertAsync<T>(T dal) where T : BaseDal<Guid>
    {
        var repository = GetRepository<T>();
        var id = await repository.InsertAsync(dal);
        return id;
    }

    /// <summary>
    /// удаляет объект типа T с переданным id
    /// </summary>
    public async Task DeleteAsync<T>(Guid id) where T : BaseDal<Guid>
    {
        var repository = GetRepository<T>();
        await repository.DeleteAsync(id);
    }
    
    /// <summary>
    /// Возвращает все объекты типа T из бд
    /// </summary>
    public List<T> GetAll<T>() where T : BaseDal<Guid>
    {
        var repository = GetRepository<T>() ;
        return repository.GetAll();
    }

    /// <summary>
    /// возвращает репозиторий, работающий с таблицей объектов типа T
    /// </summary>
    private BaseRepository<T, Guid> GetRepository<T>() where T : BaseDal<Guid>
    {
        var fieldInfo = this
            .GetType()
            .GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance)?
            .FirstOrDefault(x =>
                x.FieldType
                    .GetInterfaces()
                    .First()
                    .GenericTypeArguments
                    .Contains(typeof(T)));
        var repository = fieldInfo.GetValue(this) as BaseRepository<T, Guid>;
        return repository;
    }

    /// <summary>
    /// Создает новую запись истории задач со входящими данным и возвращет id созданной записи в бд
    /// </summary>
    private async Task CreateNewHistoryEntry(TaskDal task, ActionTypeEnum action, string? comment=null)
    {
        var history = new HistoryDal(action, task, comment);
        await _historyRepository.InsertAsync(history);
    }
    
    private async Task<UserDal> GetUserFromTokenAsync(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadToken(token) as JwtSecurityToken;
        var userId = jwt.Claims.First(x => x.Type == "userId").Value;
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            throw new UserNotFoundException(userId);
        return user;
    }

    private async Task CheckAndSetDataForTask(TaskDal taskDal, string token, Guid blockId)
    {
        var user = await GetUserFromTokenAsync(token);
        var claims = await _userManager.GetClaimsAsync(user);
        taskDal.User = user;
        var block = await _blockRepository.GetAsync(blockId);
        if (block == null)
            throw new BlockNotFoundException(blockId);
        taskDal.Block = block;
        taskDal.DepartamentId = int.Parse(claims.FirstOrDefault(x => x.Type == "DepartmentId").Value);
    }
}