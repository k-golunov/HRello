using Dal.Base.Interfaces;
using Dal.Tasks.Entities;

namespace Logic.Managers.Task.Interfaces;

public interface ITaskManager: IBaseRepository<TaskDal, Guid>
{
};

