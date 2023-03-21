﻿using Dal.Base.Entitities;

namespace Logic.Managers.Base.Interfaces;

/// <summary>
/// Интерфейс для базового мененджера
/// </summary>
/// <typeparam name="T">Модель dal</typeparam>
/// <typeparam name="TI">Тип Id (Guid или int)</typeparam>
public interface IBaseManager<T, TI> where T : BaseDal<TI>
{
    public Task<TI> InsertAsync(T dal);
    public void DeleteAsync(TI id);
    public Task<T?> GetAsync(TI id);
    public Task<TI> UpdateAsync(T dal);
}