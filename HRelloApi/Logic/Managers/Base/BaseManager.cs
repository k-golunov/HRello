using Dal.Base.Entitities;
using Dal.Base.Interfaces;
using Logic.Managers.Base.Interfaces;
using Serilog;
using Serilog.Events;

namespace Logic.Managers.Base;

/// <summary>
/// Базовый мененджер для CRUD операций
/// </summary>
/// <typeparam name="T">Модель dal</typeparam>
/// <typeparam name="TI">Тип Id (Guid или int)</typeparam>
public class BaseManager<T, TI> : IBaseManager<T, TI> where T : BaseDal<TI>
{
    protected readonly IBaseRepository<T, TI> Repository;

    public BaseManager(IBaseRepository<T, TI> repository)
    {
        Repository = repository;
    }

    public async Task<TI> InsertAsync(T dal)
    {
        Log.Logger.Write(LogEventLevel.Information, $"entity {typeof(T)} with Id {dal.Id} created");
        return await Repository.InsertAsync(dal);
    }

    public async Task DeleteAsync(TI id)
    {
        await Repository.DeleteAsync(id);
        Log.Logger.Write(LogEventLevel.Information, $"entity {typeof(TI)} with Id {id} delete");
    }

    public async Task<T?> GetAsync(TI id)
    {
        return await Repository.GetAsync(id);
    }

    public async Task<TI> UpdateAsync(T dal)
    {
        Log.Logger.Write(LogEventLevel.Information, $"entity {typeof(TI)} with Id {dal.Id} updated");
        return await Repository.UpdateAsync(dal);
    }
    
    public async Task<List<T>> GetAllAsync()
    {
        return await Repository.GetAllAsync();
    }
}