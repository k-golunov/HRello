using Dal.Base;
using Dal.TaskResult.Entities;
using Dal.TaskResult.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.TaskResult.Repositories;

/// <summary>
/// Репозиторий для работы с итогами от главного руководителя
/// </summary>
public class TaskResultRepository : BaseRepository<TaskResultDal, Guid>, ITaskResultRepository
{
    public TaskResultRepository(DataContext context) : base(context)
    {
    }

    public override async Task<TaskResultDal?> GetAsync(Guid id)
    {
        return await _dbSet.Include(dal => dal.Tasks)
            .FirstAsync(x => x.Id == id);
    }

    public override async Task<List<TaskResultDal>> GetAllAsync()
    {
        return await _dbSet.Include(dal => dal.Tasks)
            .ToListAsync();
    }
}