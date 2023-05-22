using Dal.Base.Interfaces;
using Dal.TaskResult.Entities;
using Dal.TaskResult.Repositories.Interfaces;
using Logic.Managers.Base;
using Logic.Managers.Result.Interfaces;

namespace Logic.Managers.Result;

public class TaskResultManager : BaseManager<TaskResultDal, Guid>, ITaskResultManager
{
    public TaskResultManager(ITaskResultRepository repository) : base(repository)
    {
    }
}