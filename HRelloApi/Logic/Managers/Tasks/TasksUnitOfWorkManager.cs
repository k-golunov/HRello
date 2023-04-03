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
    
    public async Task<Guid> CreateTaskAsync(TaskDal taskDal)
    {
        var historyDal = new HistoryDal(ActionTypeEnum.OnChecking, taskDal);
        taskDal.History.Add(historyDal);
        var taskId= await _taskRepository.InsertAsync(taskDal);
        return taskId;
    }

    public async Task<Guid> UpdateTaskAsync(TaskDal taskDal)
    {
        var historyDal = new HistoryDal(ActionTypeEnum.Updated, taskDal);
        var taskId = await _taskRepository.UpdateAsync(taskDal);
        await _historyRepository.InsertAsync(historyDal);
        return taskId;
    }
    
    public void ChangeStatus(TaskDal task, StatusEnum nextStatus)
    { 
        var statusNode = _statusTree.GetStatusNode(task.Status);
        if (statusNode.IsNextStatus(nextStatus))
        { 
            task.Status = nextStatus;
        }
        else
        { 
            throw new Exception();//?????Может какую-то другую обработку
        }
    }

    public async Task<TI?> GetAsync<T, TI>(Type type, Guid id) where TI : BaseDal<Guid>
    {
        var fieldInfo = this
            .GetType()
            .GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance)?
            .FirstOrDefault(x =>
                x.FieldType.GetInterfaces()[0].GenericTypeArguments.Contains(type));
        var field = fieldInfo.GetValue(this);
        //вот тут надо юы передавать вместо TaskDal BaseDal<Guid>, но тогда репозиторий становится null
        //хуй знает почему, если field то уже по сути нужный нам объект, но при это апкаст приводит его к нулю
        //var repository = field as IBaseRepository<TaskDal, Guid>;
        var instance = (BaseRepository<TI, Guid>)Activator.CreateInstance(field.GetType(), _context)!;
        //instance = instance as IBaseRepository<BaseDal<Guid>, Guid>;
        var result = await instance.GetAsync(id);
        //var dal = await repository.GetAsync(id);
        
        return result;
    }
}