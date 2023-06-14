namespace HRelloApi.Controllers.Public.Results.dto.Response;

/// <summary>
/// выдача всех итогов по блокам
/// </summary>
public record BlockTaskResultResponse
{
    /// <summary>
    /// id блока
    /// </summary>
    public Guid BlockId { get; init; }
    /// <summary>
    /// список итогов
    /// </summary>
    public List<GetTaskResultResponse> TaskResults { get; init; }
}