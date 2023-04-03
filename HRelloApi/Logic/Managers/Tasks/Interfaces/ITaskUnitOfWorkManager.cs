using Dal.Base.Entitities;
using Dal.Base.Interfaces;
using Dal.Tasks.Entities;
using Dal.Tasks.Enum;

namespace Logic.Managers.Tasks.Interfaces;

public interface ITaskUnitOfWorkManager
{
    public Task<Guid> CreateTaskAsync(TaskDal taskDal);
    public Task<Guid> UpdateTaskAsync(TaskDal taskDal);
    public void ChangeStatus(TaskDal task, StatusEnum nextStatus);

    public Task<TI?> GetAsync<T, TI>(Type type, Guid id) where TI : BaseDal<Guid>;
}