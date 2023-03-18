using Dal.Base.Interfaces;
using Dal.Entities;

namespace Logic.Managers.Departament.Interfaces;

/// <summary>
/// Интерфейс мэненджера для работы с отделами
/// </summary>
public interface IDepartamentManager : IBaseRepository<DepartamentDal, int>
{
    
}