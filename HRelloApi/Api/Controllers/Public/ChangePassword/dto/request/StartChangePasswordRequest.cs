namespace HRelloApi.Controllers.Public.ChangePassword.dto.request;

/// <summary>
/// Старт смены пароля 
/// </summary>
public record StartChangePasswordRequest
{
    /// <summary>
    /// Полчат пользователя
    /// </summary>
    public required string Email { get; init; }
}