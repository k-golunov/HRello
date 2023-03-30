using Dal.Base;
using Dal.Base.Interfaces;
using Dal.Tasks.Entities;
using Dal.Tasks.Repositories.Interfaces;

namespace Dal.Tasks.Repositories;

/// <summary>
/// Репозиторий для работы с итогами от руководителя
/// </summary>
public class BossTaskResultsRepository : BaseRepository<BossTaskResultDal, Guid>, IBossTaskResultsRepository
{
    public BossTaskResultsRepository(DataContext context) : base(context)
    {
    }
}