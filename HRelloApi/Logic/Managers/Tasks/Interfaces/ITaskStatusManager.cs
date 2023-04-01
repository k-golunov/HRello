using Dal.Base.Interfaces;
using Dal.Tasks.Entities;
using Dal.Tasks.Enum;

namespace Logic.Managers.Tasks.Interfaces;

public interface ITaskStatusManager: IBaseRepository<TaskDal, Guid>
{
    public void ChangeStatus(TaskDal task, StatusEnum nextStatus);
}