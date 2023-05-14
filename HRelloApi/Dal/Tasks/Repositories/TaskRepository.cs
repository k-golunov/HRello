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

    public override List<TaskDal> GetAll()
    {
        return _dbSet
                .Include(x => x.Block)
                .Include(x => x.User)
                .ToList();
    }
}