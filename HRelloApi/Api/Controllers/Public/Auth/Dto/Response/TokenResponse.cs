using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.Auth.Dto.Response;

/// <summary>
/// Модель для возврата jwt токенов
/// </summary>
public record TokenResponse
{
    /// <summary>
    /// Аксес токен
    /// </summary>
    [JsonProperty("access_token")]
    public string AccessToken { get; init; }
    
    /// <summary>
    /// Рефреш токен
    /// </summary>
    [JsonProperty("refresh_token")]
    public string RefreshToken { get; init; }
}