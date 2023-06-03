using Dal.Base;
using Dal.Tasks.Entities;
using Dal.Tasks.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dal.Tasks.Repositories;

public class TaskRepository: BaseRepository<TaskDal, Guid>, ITaskRepository
{
    public TaskRepository(DataContext context): base(context)
    {
        
    }

    public override async Task<TaskDal?> GetAsync(Guid id)
    {
        return await _dbSet
            .Include(x => x.User)
            .Include(x => x.Block)
            .FirstAsync(x => x.Id == id);
    }

    public override async Task<List<TaskDal>> GetAllAsync()
    {
        return await _dbSet
                .Include(x => x.Block)
                .Include(x => x.User)
                .ToListAsync();
    }

    public async Task DeleteAllAsync()
    {
        _dbSet.RemoveRange(_dbSet.ToList());
        await _context.SaveChangesAsync();
    }

    public async Task<List<TaskDal>> GetAllWithResult(int year, List<int> quarter)
    {
        var result = await _dbSet.Include(x => x.UserTaskResultDal)
            .Include(x => x.BossTaskResultDal)
            .Include(x => x.User)
            .Include(x => x.Block)
            .Where(x => x.Year == year)
            .Where(x => quarter.Contains(x.Quarter))
            .ToListAsync();
        return result;
    }
}