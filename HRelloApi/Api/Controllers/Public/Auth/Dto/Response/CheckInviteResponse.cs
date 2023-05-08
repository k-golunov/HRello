using Newtonsoft.Json;
using Org.BouncyCastle.Ocsp;

namespace HRelloApi.Controllers.Public.Auth.Dto.Response;

/// <summary>
/// Модель для ответа, есть ли у пользователя инфвйт в систему
/// </summary>
public record CheckInviteResponse
{
    /// <summary>
    /// Есть ли инвайт
    /// </summary>
    [JsonProperty("isInvite")]
    public required bool IsInvite { get; init; }
}