using Dal.Base.Interfaces;
using Dal.Tasks.Entities;

namespace Dal.Tasks.Repositories.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с задачами
/// </summary>
public interface ITaskRepository: IBaseRepository<TaskDal, Guid>
{
    public Task DeleteAllAsync();

    public Task<List<TaskDal>> GetAllWithResult(int year, List<int> quarter);
}