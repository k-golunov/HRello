using Dal.Base.Interfaces;
using Dal.TaskResult.Entities;

namespace Logic.Managers.Result.Interfaces;

/// <summary>
/// Мэненджер для итогов от гл руководителя
/// </summary>
public interface ITaskResultManager : IBaseRepository<TaskResultDal, Guid>
{
    
}