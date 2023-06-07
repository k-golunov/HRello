namespace HRelloApi.Hubs.Tasks.Messages;

/// <summary>
/// 
/// </summary>
public record UpdateTaskMessage
{
    /// <summary>
    /// Идентификатор задачи
    /// </summary>
    public required Guid TaskId { get; init; }
    
    /// <summary>
    /// Сообщение
    /// </summary>
    public required string Message { get; init; }
}