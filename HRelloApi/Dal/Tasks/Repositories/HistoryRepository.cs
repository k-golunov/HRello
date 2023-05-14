using Dal.Base;
using Dal.Tasks.Entities;
using Dal.Tasks.Repositories.Interfaces;

namespace Dal.Tasks.Repositories;

/// <summary>
/// Репозиторий для работы с историей изменения задачи
/// </summary>
public class HistoryRepository : BaseRepository<HistoryDal, Guid>, IHistoryRepository
{
    public HistoryRepository(DataContext context) : base(context)
    {
    }

    public List<HistoryDal> GetAllHistoryByTaskId(Guid taskId)
    {
        return _dbSet.Where(h => h.Task.Id == taskId).ToList();
    }
}