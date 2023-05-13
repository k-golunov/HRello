using Dal.Base.Interfaces;
using Dal.Tasks.Entities;
using Dal.Tasks.Repositories.Interfaces;

namespace Logic.Managers.History.Interfaces;

/// <summary>
/// Мэненджер для историй задачи
/// </summary>
public interface IHistoryManager
{
    /// <summary>
    /// Получение всей истории по задаче
    /// </summary>
    /// <param name="taskId">идентификатор задачи</param>
    /// <returns></returns>
    List<HistoryDal> GetAllHistoryByTaskId(Guid taskId);
}