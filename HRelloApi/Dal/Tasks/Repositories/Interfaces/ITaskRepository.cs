using Dal.Base.Interfaces;
using Dal.Tasks.Entities;

namespace Dal.Tasks.Repositories.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с задачами
/// </summary>
public interface ITaskRepository: IBaseRepository<TaskDal, Guid>
{
    public Task DeleteAll();
}