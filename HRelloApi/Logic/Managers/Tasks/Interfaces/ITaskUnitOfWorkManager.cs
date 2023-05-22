using Dal.Base.Entitities;
using Dal.Base.Interfaces;
using Dal.Entities;
using Dal.Tasks.Entities;
using Dal.Tasks.Enum;

namespace Logic.Managers.Tasks.Interfaces;

/// <summary>
/// Интерфейс основного мэнэджера для логики запросов связанных с задачами
/// </summary>
public interface ITaskUnitOfWorkManager
{
    public Task<Guid> CreateTaskAsync(TaskDal taskDal, Guid blockId, string token);
    public Task<Guid> UpdateTaskAsync(TaskDal taskDal,Guid blockId, string token, string? comment);
    public List<TaskDal> ApplyFilters(Filters.Filters filters, List<TaskDal> tasks);
    public Task<Guid> ChangeStatus(Guid taskId, StatusEnum nextStatus, string comment);

    public Task<Guid> SendResultForTask<T>(T taskResult, TaskDal task, StatusEnum status, string comment = null)
        where T : BaseDal<Guid>;
    public Task<T?> GetAsync<T>(Guid id) where T : BaseDal<Guid>;
    public Task<Guid> UpdateAsync<T>(T dal) where T : BaseDal<Guid>;
    public Task<Guid> InsertAsync<T>(T dal) where T : BaseDal<Guid>;
    public Task DeleteAsync<T>(Guid id) where T : BaseDal<Guid>;
    public Task<List<T>> GetAllAsync<T>() where T : BaseDal<Guid>;
}