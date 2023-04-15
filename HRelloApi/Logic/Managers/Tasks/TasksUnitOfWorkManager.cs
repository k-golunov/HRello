using System.Dynamic;
using System.Reflection;
using Dal;
using Dal.Base;
using Dal.Base.Entitities;
using Dal.Base.Interfaces;
using Dal.Tasks.Entities;
using Dal.Tasks.Enum;
using Dal.Tasks.Repositories;
using Dal.Tasks.Repositories.Interfaces;
using Logic.Managers.Tasks.Interfaces;
using Logic.Managers.Tasks.StatusesTree;

namespace Logic.Managers.Tasks;

/// <summary>
/// Реализация паттерна UnitOfWork для task и всех связанных сущностей, кроме юзера
/// Возможно еще и юзера
/// Возможно сюда включать также статус мененджер или репозиторий
/// </summary>
public class TaskUnitOfWorkManager : ITaskUnitOfWorkManager
{
    private readonly ITaskRepository _taskRepository;
    private readonly IHistoryRepository _historyRepository;
    private readonly IBossTaskResultsRepository _bossTaskResultsRepository;
    private readonly IUserTaskResultsRepository _userTaskResultsRepository;
    private readonly StatusTree _statusTree;
    private readonly DataContext _context;
    
    public TaskUnitOfWorkManager(ITaskRepository taskRepository, IHistoryRepository historyRepository, 
        IBossTaskResultsRepository bossTaskResultsRepository, IUserTaskResultsRepository userTaskResultsRepository,
        StatusTree statusTree, DataContext context)
    {
        _taskRepository = taskRepository;
        _historyRepository = historyRepository;
        _userTaskResultsRepository = userTaskResultsRepository;
        _bossTaskResultsRepository = bossTaskResultsRepository;
        _statusTree = statusTree;
        _context = context;
    }
    
    /// <summary>
    /// Операция создания новой задачи
    /// </summary>
    /// <param name="taskDal">сущность, создаваемой задачи</param>
    /// <returns>id созданной записи</returns>
    public async Task<Guid> CreateTaskAsync(TaskDal taskDal)
    {
        var historyDal = new HistoryDal(ActionTypeEnum.OnChecking, taskDal);
        taskDal.History.Add(historyDal);
        var taskId= await _taskRepository.InsertAsync(taskDal);
        return taskId;
    }

    /// <summary>
    /// обновляет данные о задаче
    /// </summary>
    /// <param name="taskDal">сущность задачи с новыми данными</param>
    /// <returns>id обновленной записи</returns>
    public async Task<Guid> UpdateTaskAsync(TaskDal taskDal)
    {
        var historyDal = new HistoryDal(ActionTypeEnum.Updated, taskDal);
        var taskId = await _taskRepository.UpdateAsync(taskDal);
        await _historyRepository.InsertAsync(historyDal);
        return taskId;
    }
    
    /// <summary>
    /// изменяет статус задачи на входной
    /// </summary>
    /// <param name="task">задача</param>
    /// <param name="nextStatus">новый статус</param>
    public async Task<bool> IsChangeStatus(TaskDal task, StatusEnum nextStatus)
    { 
        var statusNode = _statusTree.GetStatusNode(task.Status);
        if (statusNode.IsNextStatus(nextStatus))
        { 
            task.Status = nextStatus;
            await _taskRepository.UpdateAsync(task);
            return true;
        }

        return false;
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
        var repository = GetRepository<T>();
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
}