using Dal.Base.Entitities;
using Dal.Tasks.Entities;
using Dal.Tasks.Enum;

namespace Logic.Managers.Tasks.Interfaces;

public interface ITaskUnitOfWorkManager
{
    public Task<Guid> CreateTaskAsync(TaskDal taskDal);
    public Task<Guid> UpdateTaskAsync(TaskDal taskDal);
    public void ChangeStatus(TaskDal task, StatusEnum nextStatus);

    public Task<BaseDal<Guid>> GetAsync<T>(Type type, Guid id);
}