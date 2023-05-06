namespace HRelloApi.Controllers.Public.Block.dto.request;

/// <summary>
/// модель данных на запрос редактирования блока задач
/// </summary>
public class UpdateBlockRequest
{
    /// <summary>
    /// id блока
    /// </summary>
    public required Guid Id { get; init; }
    
    /// <summary>
    /// значение блока
    /// </summary>
    public required string Value { get; init; }
}