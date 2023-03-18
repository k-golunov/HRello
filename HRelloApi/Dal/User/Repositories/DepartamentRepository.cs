using Dal.Base;
using Dal.Entities;
using Dal.User.Repositories.Interfaces;

namespace Dal.User.Repositories;

/// <summary>
/// Репозиторий для работы с отделами
/// </summary>
public class DepartamentRepository : BaseRepository<DepartamentDal, int>, IDepartamentRepository 
{
    public DepartamentRepository(DataContext context) : base(context)
    {
    }
}