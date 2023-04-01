using Dal.Base;
using Dal.Tasks.Entities;
using Dal.Tasks.Repositories.Interfaces;

namespace Dal.Tasks.Repositories;

/// <summary>
/// Репозиторий для работы с итогами от сотрудника
/// </summary>
public class UserTaskResultsRepository : BaseRepository<UserTaskResultDal, Guid>, IUserTaskResultsRepository
{
    public UserTaskResultsRepository(DataContext context) : base(context)
    {
    }
}