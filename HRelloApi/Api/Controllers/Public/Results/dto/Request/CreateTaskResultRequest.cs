using Dal.TaskResult.Enums;
using Dal.Tasks.Entities;

namespace HRelloApi.Controllers.Public.Results.dto.Request;

/// <summary>
/// Модель для создания итога
/// </summary>
public record CreateTaskResultRequest
{
    /// <summary>
    /// Текстовый итог
    /// </summary>
    public required string Result { get; init; }
    
    /// <summary>
    /// Цвет задачи
    /// </summary>
    public required ColorEnum Color { get; init; }
    
    /// <summary>
    /// Год итога
    /// </summary>
    public required int Year { get; init; }
    
    /// <summary>
    /// Квартал итога
    /// </summary>
    public required int Quarter { get; init; }

    /// <summary>
    /// Связанные задачи 
    /// </summary>
    public required List<Guid> TasksId { get; init; } = new();
}
