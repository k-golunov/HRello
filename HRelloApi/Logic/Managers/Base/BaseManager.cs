using Dal.Base.Entitities;
using Dal.Base.Interfaces;
using Logic.Exceptions.Base;
using Logic.Managers.Base.Interfaces;
using Serilog;
using Serilog.Events;

namespace Logic.Managers.Base;

/// <summary>
/// Базовый мененджер для CRUD операций
/// </summary>
/// <typeparam name="T">Модель dal</typeparam>
/// <typeparam name="TI">Тип Id (Guid или int)</typeparam>
public abstract class BaseManager<T, TI> : IBaseManager<T, TI> where T : BaseDal<TI>
{
    private readonly IBaseRepository<T, TI> Repository;

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
        var response = await Repository.GetAsync(id);
        if (response is null)
        {
            throw new NotFoundEntitiesException(typeof(T).Name);
        }
        
        return response;
    }

    public async Task<TI> UpdateAsync(T dal)
    {
        Log.Logger.Write(LogEventLevel.Information, $"entity {typeof(TI)} with Id {dal.Id} updated");
        return await Repository.UpdateAsync(dal);
    }
    
    public async Task<List<T>> GetAllAsync()
    {
        var response = await Repository.GetAllAsync();
        if (response.Count == 0)
        {
            throw new NotFoundEntitiesException(typeof(T).Name);
        }
        
        return response;
    }

    public async Task<List<T>> GetByListIdAsync(List<TI> listId)
    {
        var response = await Repository.GetByListIdAsync(listId);
        if (response.Count == 0)
        {
            throw new NotFoundEntitiesException(typeof(T).Name);
        }
        
        return response;
    }
}