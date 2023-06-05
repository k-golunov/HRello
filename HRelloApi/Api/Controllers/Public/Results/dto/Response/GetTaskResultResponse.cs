using Dal.TaskResult.Enums;
using Dal.Tasks.Entities;

namespace HRelloApi.Controllers.Public.Results.dto.Response;

/// <summary>
/// Модель ответа для получения итога
/// </summary>
public record GetTaskResultResponse
{
    /// <summary>
    /// Идентификатор итога
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Текстовый итог
    /// </summary>
    public string Result { get; init; }
    
    /// <summary>
    /// Цвет задачи
    /// </summary>
    public ColorEnum Color { get; init; }
    
    /// <summary>
    /// Год итога
    /// </summary>
    public int Year { get; init; }
    
    /// <summary>
    /// Квартал итога
    /// </summary>
    public int Quarter { get; init; }

    /// <summary>
    /// Связанные задачи 
    /// </summary>
    public List<TaskDal> Tasks { get; init; } = new();
}