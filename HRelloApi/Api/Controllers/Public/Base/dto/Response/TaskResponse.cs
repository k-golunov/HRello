using Dal.Tasks.Entities;

namespace HRelloApi.Controllers.Public.Base.dto.Response;

/// <summary>
/// Модель ответа для возвращения задачи
/// </summary>
public record TaskResponse
{
    /// <summary>
    /// задача
    /// </summary>
    public required TaskDal Task { get; init; }
}