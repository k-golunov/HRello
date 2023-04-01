using Dal.Tasks.Entities;

namespace Logic.Managers.Tasks.Interfaces;

public interface ITaskUnitOfWorkManager
{
    public Task<Guid> CreateTaskAsync(TaskDal taskDal);
    public Task<Guid> UpdateTaskAsync(TaskDal taskDal);
}