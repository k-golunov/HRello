using Dal.Base.Interfaces;
using Dal.Tasks.Entities;

namespace Logic.Managers.Task.Interfaces;

public interface ITaskStatusManager: IBaseRepository<TaskDal, Guid>
{
    public System.Threading.Tasks.Task ChangeStatus(Guid taskId, bool isNext);
}