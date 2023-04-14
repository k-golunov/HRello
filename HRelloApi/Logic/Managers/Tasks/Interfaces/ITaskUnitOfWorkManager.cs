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

    public Task<T?> GetAsync<T>(Guid id) where T : BaseDal<Guid>;
    public Task<Guid> UpdateAsync<T>(T dal) where T : BaseDal<Guid>;
    public Task<Guid> InsertAsync<T>(T dal) where T : BaseDal<Guid>;
    public Task DeleteAsync<T>(Guid id) where T : BaseDal<Guid>;
    public List<T> GetAll<T>() where T : BaseDal<Guid>;
}