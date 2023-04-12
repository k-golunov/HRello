using Dal.Tasks.Entities;

namespace HRelloApi.Controllers.Public.Base.dto.Response;

/// <summary>
/// Модель ответа для возвращения всех задач
/// </summary>
public record AllTasksResponse
{
    /// <summary>
    /// лист задач
    /// </summary>
    public required List<TaskDal> Tasks { get; init; }
}