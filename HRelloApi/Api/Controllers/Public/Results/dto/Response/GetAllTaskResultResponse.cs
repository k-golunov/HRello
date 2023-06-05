namespace HRelloApi.Controllers.Public.Results.dto.Response;

/// <summary>
/// получение всех итогов
/// </summary>
public record GetAllTaskResultResponse
{
    /// <summary>
    /// Все итоги
    /// </summary>
    public List<GetTaskResultResponse> AllTaskResultResponse { get; init; }
}