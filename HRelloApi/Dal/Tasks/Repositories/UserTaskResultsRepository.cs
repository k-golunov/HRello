using Dal.Base;
using Dal.Tasks.Entities;

namespace Dal.Tasks.Repositories;

/// <summary>
/// Репозиторий для работы с итогами от сотрудника
/// </summary>
public class UserTaskResultsRepository : BaseRepository<UserTaskResultDal, Guid>
{
    public UserTaskResultsRepository(DataContext context) : base(context)
    {
    }
}