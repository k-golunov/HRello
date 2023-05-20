using Dal.Base.Interfaces;
using Dal.TaskResult.Entities;

namespace Dal.TaskResult.Repositories.Interfaces;

public interface ITaskResultRepository : IBaseRepository<TaskResultDal, Guid>
{
    
}