using Dal.Tasks.Entities;
using Dal.Tasks.Repositories.Interfaces;
using Logic.Managers.Base;
using Logic.Managers.Task.Interfaces;

namespace Logic.Managers.Task;

public class TaskManager: BaseManager<TaskDal, Guid>, ITaskManager
{
    public TaskManager(ITaskRepository repository) : base(repository)
    {
    }
}