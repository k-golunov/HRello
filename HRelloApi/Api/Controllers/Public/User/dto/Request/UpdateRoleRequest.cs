namespace HRelloApi.Controllers.Public.User.dto.Request;

/// <summary>
/// Запрос на обновление роли пользователя
/// </summary>
public class UpdateRoleRequest
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public required string UserId { get; init; }

    /// <summary>
    /// Старая роль (на удаление)
    /// </summary>
    public required string OldRole { get; init; }
    
    /// <summary>
    /// Новая роль
    /// </summary>
    public required string NewRole { get; init; }
}