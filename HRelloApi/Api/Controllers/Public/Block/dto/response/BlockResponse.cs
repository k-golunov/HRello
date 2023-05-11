namespace HRelloApi.Controllers.Public.Block.dto.response;

/// <summary>
/// модель ответа на запросы операций с блоками задач
/// </summary>
public class BlockResponse
{
    /// <summary>
    /// id блока
    /// </summary>
    public Guid Id { get; init; }
    
    /// <summary>
    /// значение блока
    /// </summary>
    public string Value { get; init; }

    /// <summary>
    /// Конструктор
    /// </summary>
    public BlockResponse(Guid id, string value)
    {
        Id = id;
        Value = value;
    }
}