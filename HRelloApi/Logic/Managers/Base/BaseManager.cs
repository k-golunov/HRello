using Dal.Base.Entitities;
using Dal.Base.Interfaces;
using Logic.Managers.Base.Interfaces;

namespace Logic.Managers.Base;

/// <summary>
/// Базовый мененджер для CRUD операций
/// </summary>
/// <typeparam name="T">Модель dal</typeparam>
/// <typeparam name="TI">Тип Id (Guid или int)</typeparam>
public class BaseManager<T, TI> : IBaseManager<T, TI> where T : BaseDal<TI>
{
    private readonly IBaseRepository<T, TI> _repository;

    public BaseManager(IBaseRepository<T, TI> repository)
    {
        _repository = repository;
    }

    public async Task<TI> InsertAsync(T dal)
    {
        return await _repository.InsertAsync(dal);
    }

    public void DeleteAsync(TI id)
    {
        _repository.DeleteAsync(id);
    }

    public async Task<T?> GetAsync(TI id)
    {
        return await _repository.GetAsync(id);
    }

    public async Task<TI> UpdateAsync(T dal)
    {
        return await _repository.UpdateAsync(dal);
    }
}