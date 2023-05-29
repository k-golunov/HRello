namespace HRelloApi.Controllers.Public.ChangePassword.dto.request;

/// <summary>
/// Смена пароля
/// </summary>
public record ChangePasswordRequest
{
    /// <summary>
    /// Новый пароль
    /// </summary>
    public required string NewPassword { get; init; }
}