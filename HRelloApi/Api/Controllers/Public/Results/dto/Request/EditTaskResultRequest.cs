using Dal.TaskResult.Enums;
using Dal.Tasks.Entities;

namespace HRelloApi.Controllers.Public.Results.dto.Request;

/// <summary>
/// Модель для редактирования итога
/// </summary>
public record EditTaskResultRequest
{
    /// <summary>
    /// Id итога
    /// </summary>
    public required Guid Id { get; init; }
    /// <summary>
    /// Текстовое описание итога
    /// </summary>
    public required string Result { get; init; }
    /// <summary>
    /// Статус итога
    /// </summary>
    public required ColorEnum Color { get; init; }

    /// <summary>
    /// Список задач, входящих в итог
    /// </summary>
    public required List<Guid> TasksId { get; init; } = new();
}
;

