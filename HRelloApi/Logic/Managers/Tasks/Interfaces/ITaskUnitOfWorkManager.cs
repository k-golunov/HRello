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
    Task<Guid> UpdateTaskAsync(TaskDal taskDal, Guid blockId, string token, string userId, string? comment);
    public List<TaskDal> ApplyFilters(Filters.Filters filters, List<TaskDal> tasks);
    Task<Guid> ChangeStatus(Guid taskId, StatusEnum nextStatus, string userId, string comment);

    Task<Guid> SendResultForTask<T>(T taskResult, TaskDal task, StatusEnum status, string userId, string comment = null)
        where T : BaseDal<Guid>;
    public Task<T?> GetAsync<T>(Guid id) where T : BaseDal<Guid>;
    /// <summary>
    /// получение массива данных по массиву айдишников
    /// </summary>
    /// <param name="listId">массив айдишников</param>
    /// <typeparam name="T">Тип сущности</typeparam>
    /// <returns></returns>
    public Task<List<T>> GetByListIdAsync<T>(List<Guid> listId) where T : BaseDal<Guid>;
    public Task<Guid> UpdateAsync<T>(T dal) where T : BaseDal<Guid>;
    public Task<Guid> InsertAsync<T>(T dal) where T : BaseDal<Guid>;
    public Task DeleteAsync<T>(Guid id) where T : BaseDal<Guid>;
    public Task<List<T>> GetAllAsync<T>() where T : BaseDal<Guid>;

    /// <summary>
    /// Метод для получения байт для скачивания эксель файла
    /// </summary>
    /// <param name="year">год задач</param>
    /// <param name="quarter">квартал задач</param>
    /// <returns></returns>
    public Task<byte[]> GetExcelFile(int year, List<int> quarter);
}