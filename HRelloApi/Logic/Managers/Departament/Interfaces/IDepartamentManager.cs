using Dal.Base.Interfaces;
using Dal.Entities;
using Dal.User.Models;

namespace Logic.Managers.Departament.Interfaces;

/// <summary>
/// Интерфейс мэненджера для работы с отделами
/// </summary>
public interface IDepartamentManager : IBaseRepository<DepartamentDal, int>
{
    public int Test();
}