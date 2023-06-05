using Dal.Base.Interfaces;
using Dal.TaskResult.Entities;

namespace Dal.Results.Repositories.Interfaces;

public interface IResultRepository : IBaseRepository<TaskResultDal, Guid>
{
    
}