using Dal.Base;
using Dal.Tasks.Entities;
using Dal.Tasks.Repositories.Interfaces;

namespace Dal.Tasks.Repositories;

public class TaskRepository: BaseRepository<TaskDal, Guid>, ITaskRepository
{
    public TaskRepository(DataContext context): base(context)
    {
        
    }
}