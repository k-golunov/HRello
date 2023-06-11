using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using AutoMapper;
using Dal;
using Dal.Base;
using Dal.Base.Entitities;
using Dal.Base.Interfaces;
using Dal.Entities;
using Dal.Tasks.Entities;
using Dal.Tasks.Enum;
using Dal.Tasks.Repositories;
using Dal.Tasks.Repositories.Interfaces;
using Logic.Excel;
using Logic.Exceptions.Base;
using Logic.Exceptions.Tasks;
using Logic.Exceptions.User;
using Logic.Managers.Tasks.Filters;
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
    private readonly IMapper _mapper;

    /// <summary>
    /// Конструтор
    /// </summary>
    public TaskUnitOfWorkManager(
        ITaskRepository taskRepository, 
        IHistoryRepository historyRepository, 
        IBossTaskResultsRepository bossTaskResultsRepository, 
        IUserTaskResultsRepository userTaskResultsRepository,
        IBlockRepository blockRepository,
        UserManager<UserDal> userManager,
        IMapper mapper)
    {
        _taskRepository = taskRepository;
        _historyRepository = historyRepository;
        _userTaskResultsRepository = userTaskResultsRepository;
        _bossTaskResultsRepository = bossTaskResultsRepository;
        _blockRepository = blockRepository;
        _userManager = userManager;
        _mapper = mapper;
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
        await CreateNewHistoryEntry(taskDal, ActionTypeEnum.OnChecking, taskDal.UserId);
        return taskDal.Id;
    }

    /// <summary>
    /// обновляет данные о задаче
    /// </summary>
    /// <param name="taskDal">сущность задачи с новыми данными</param>
    /// <param name="blockId">id обновленного блока задачи</param>
    /// <param name="token">jwt пользователя</param>
    /// <param name="userId">Идентификатор пользователя, который внес изменения</param>
    /// <returns>id обновленной записи</returns>
    public async Task<Guid> UpdateTaskAsync(TaskDal taskDal, Guid blockId, string token, string userId ,string? comment)
    {
        await CheckAndSetDataForTask(taskDal, token, blockId);
        var taskId = await _taskRepository.UpdateAsync(taskDal);
        var action = await ChangeStatusAndGetAction(taskDal, StatusEnum.OnChecking);
        await CreateNewHistoryEntry(taskDal, action, userId ,comment);
        return taskId;
    }

    /// <summary>
    /// Возвращает список задач, соотвествующих входящим фильтрам
    /// </summary>
    public List<TaskDal> ApplyFilters(Filters.Filters filters, List<TaskDal> tasksDals)
    {
        var tasks = tasksDals.Select(_mapper.Map<FilteredTask>).ToList();
        foreach (var filter in filters.GetType().GetProperties())
        {
            var value = filter.GetValue(filters);
            if (value != null)
                tasks = ApplyFilter(tasks, filter.Name, value.ToString().Split(", "));
        }

        var filteredTasksId = tasks.Select(x => x.Id).ToList();
        var f = tasksDals.Where(t => filteredTasksId.Contains(t.Id)).ToList();
        return f;
    }

    /// <summary>
    /// Возвращает список задач, соответсвующих одному входящему фильтру
    /// </summary>
    private List<FilteredTask> ApplyFilter(List<FilteredTask> tasks, string field, string[] filters)
    {
        var filteredTasks = tasks
            .Where(x => 
                filters.Contains(typeof(FilteredTask)
                    .GetProperty(field)?
                    .GetValue(x)?
                    .ToString()))
            .ToList();
        return filteredTasks;
    }

    public async Task<Guid> ChangeStatus(Guid taskId, StatusEnum nextStatus, string userId, string comment)
    {
        var task = await _taskRepository.GetAsync(taskId);
        if (task == null)
            throw new TaskNotFoundException(taskId);
        var action = await ChangeStatusAndGetAction(task, nextStatus);
        await CreateNewHistoryEntry(task, action, userId, comment);
        return task.Id;
    }

    public async Task<Guid> SendResultForTask<T>(T taskResult, TaskDal task, StatusEnum status, string userId, string comment = null) where T: BaseDal<Guid>
    {
        var result = await InsertAsync(taskResult);
        var action = await ChangeStatusAndGetAction(task, status);
        await CreateNewHistoryEntry(task, action, userId, comment);
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

    public async Task<List<T>> GetByListIdAsync<T>(List<Guid> listId) where T : BaseDal<Guid>
    {
        var repository = GetRepository<T>();
        var response = await repository.GetByListIdAsync(listId);

        if (response.Count == 0)
        {
            throw new BaseException("EntitiesNotFound", "данных не найдено", 400);
        }

        return response;
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
    public async Task<List<T>> GetAllAsync<T>() where T : BaseDal<Guid>
    {
        var repository = GetRepository<T>();
        var allData = await repository.GetAllAsync();
        // if (allData.Count == 0)
        // {
        //     throw new BaseException("EntityNotFoundException", "ни одной сущности не найдено", 400);
        // }
        
        return allData;
    }

    public async Task<byte[]> GetExcelFile(int year, List<int> quarter)
    {
        var tasksList = await _taskRepository.GetAllWithResult(year, quarter);

        var bytes = new ExcelGenerator().GenerateTasksReport(tasksList);

        return bytes;
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
    private async Task CreateNewHistoryEntry(TaskDal task, ActionTypeEnum action,string userId ,string? comment=null)
    {
        var history = new HistoryDal(action, task, userId, comment);
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