namespace HRelloApi.Controllers.Public.Results.dto.Response;

/// <summary>
/// Ответ по айди
/// </summary>
public record IdResponse
{
    /// <summary>
    /// идентификатор итога
    /// </summary>
    public Guid Id { get; init; }
}