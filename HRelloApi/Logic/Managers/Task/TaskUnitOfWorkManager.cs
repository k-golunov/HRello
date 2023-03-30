using Dal.Tasks.Repositories.Interfaces;
using Logic.Managers.Task.Interfaces;

namespace Logic.Managers.Task;

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
}