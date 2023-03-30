using Dal.Base.Interfaces;
using Dal.Tasks.Entities;

namespace Dal.Tasks.Repositories.Interfaces;

/// <summary>
/// Интерфейс для работы с итогами от руководителя
/// </summary>
public interface IBossTaskResultsRepository : IBaseRepository<BossTaskResultDal, Guid>
{
    
}