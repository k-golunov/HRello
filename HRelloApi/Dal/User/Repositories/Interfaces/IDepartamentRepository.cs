using Dal.Base.Interfaces;
using Dal.Entities;

namespace Dal.User.Repositories.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с отделами
/// </summary>
public interface IDepartamentRepository : IBaseRepository<DepartamentDal, int>
{
    
}