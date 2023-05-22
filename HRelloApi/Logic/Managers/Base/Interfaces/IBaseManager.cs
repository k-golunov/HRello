using Dal.Base.Entitities;

namespace Logic.Managers.Base.Interfaces;

/// <summary>
/// Интерфейс для базового мененджера
/// </summary>
/// <typeparam name="T">Модель dal</typeparam>
/// <typeparam name="TI">Тип Id (Guid или int)</typeparam>
public interface IBaseManager<T, TI> where T : BaseDal<TI>
{
    /// <summary>
    /// Вставка данных в бд
    /// </summary>
    /// <param name="dal">сущность</param>
    /// <returns></returns>
    public Task<TI> InsertAsync(T dal);
    
    /// <summary>
    /// Удаление данных из бд
    /// </summary>
    /// <param name="id">идентификатор сущности</param>
    /// <returns></returns>
    public Task DeleteAsync(TI id);
    
    /// <summary>
    /// Получение сущности
    /// </summary>
    /// <param name="id">идентификатор сущности</param>
    /// <returns></returns>
    public Task<T?> GetAsync(TI id);
    
    /// <summary>
    /// Обновление сущности
    /// </summary>
    /// <param name="dal">обновленная сущность (id не меняется)</param>
    /// <returns></returns>
    public Task<TI> UpdateAsync(T dal);
    
    /// <summary>
    /// Получение всех записей из бд
    /// </summary>
    /// <returns></returns>
    public Task<List<T>> GetAllAsync();
    
    /// <summary>
    /// Получение всех данных по массиву айдишников
    /// </summary>
    /// <param name="listId">массив айдишников</param>
    /// <returns></returns>
    public Task<List<T>> GetByListIdAsync(List<TI> listId);
}