using Dal.Base.Interfaces;
using Dal.TaskResult.Entities;
using Dal.Tasks.Entities;

namespace Logic.Managers.Result.Interfaces;

/// <summary>
/// Мэненджер для итогов от гл руководителя
/// </summary>
public interface IResultManager : IBaseRepository<TaskResultDal, Guid>
{
    /// <summary>
    /// Валидация год и квартала, чтобы все задачи соответствовали этому году и кварталу
    /// </summary>
    /// <param name="tasks"></param>
    /// <param name="year"></param>
    /// <param name="quarter"></param>
    /// <returns></returns>
    public void ValidationData(List<TaskDal> tasks, int year, int quarter);

    /// <summary>
    /// Генерация файла из итогов в эксель
    /// </summary>
    /// <returns></returns>
    Task<byte[]> GenerateFileAsync(int year, string quarters, string departmetnId);
}