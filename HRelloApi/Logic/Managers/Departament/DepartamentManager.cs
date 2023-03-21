﻿using Dal.Base.Interfaces;
using Dal.Entities;
using Dal.User.Repositories.Interfaces;
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
}