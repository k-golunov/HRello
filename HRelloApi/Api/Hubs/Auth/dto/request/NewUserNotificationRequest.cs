namespace HRelloApi.Hubs.Auth.dto.request;

/// <summary>
/// 
/// </summary>
public record NewUserNotificationRequest
{
    /// <summary>
    /// 
    /// </summary>
    public required string UserId { get; init; }
}