using Dal.Base.Interfaces;
using Dal.Entities;
using Dal.User.Models;
using Dal.User.Repositories.Interfaces;
using Logic.Exceptions.Base;
using Logic.Managers.Base;
using Logic.Managers.Departament.Interfaces;

namespace Logic.Managers.Departament;

/// <summary>
/// Мэненджер для работы с отделами
/// </summary>
public class DepartamentManager : BaseManager<DepartamentDal, int>, IDepartamentManager
{
    public DepartamentManager(IDepartamentRepository repository) : base(repository)
    {
    }

    public int Test()
    {
        throw new BaseException("TestException.1234", "test exception", StatusCodes.Status409Conflict);
    }
}