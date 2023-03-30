using Dal.Base.Interfaces;
using Dal.Tasks.Entities;

namespace Dal.Tasks.Repositories.Interfaces;

/// <summary>
/// Интерфейс для работы с итогами от сотрудника
/// </summary>
public interface IUserTaskResultsRepository : IBaseRepository<UserTaskResultDal, Guid>
{
    
}