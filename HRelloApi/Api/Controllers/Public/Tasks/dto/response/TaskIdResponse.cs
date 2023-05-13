namespace HRelloApi.Controllers.Public.Tasks.dto.response;

/// <summary>
/// Модель ответа на запросы создания или редактирования задачи
/// </summary>
public class TaskIdResponse
{
    /// <summary>
    /// Id задачи
    /// </summary>
    public required Guid Id { get; init; }
}