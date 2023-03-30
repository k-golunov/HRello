using Dal.Base.Interfaces;
using Dal.Tasks.Entities;

namespace Dal.Tasks.Repositories.Interfaces;

/// <summary>
/// Интерфейс для работы с историей изменения задачи
/// </summary>
public interface IHistoryRepository : IBaseRepository<HistoryDal, Guid>
{
    
}