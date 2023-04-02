using Dal.Base.Entitities;
using Dal.Tasks.Entities;
using Dal.Tasks.Enum;
using Dal.Tasks.Repositories.Interfaces;
using Logic.Managers.Tasks.Interfaces;

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
    
    public TaskUnitOfWorkManager(ITaskRepository taskRepository, IHistoryRepository historyRepository, 
        IBossTaskResultsRepository bossTaskResultsRepository, IUserTaskResultsRepository userTaskResultsRepository)
    {
        _taskRepository = taskRepository;
        _historyRepository = historyRepository;
        _userTaskResultsRepository = userTaskResultsRepository;
        _bossTaskResultsRepository = bossTaskResultsRepository;
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
}