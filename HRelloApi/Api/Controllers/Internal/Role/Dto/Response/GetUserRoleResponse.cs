namespace HRelloApi.Controllers.Internal.Role.Dto.Response;

/// <summary>
/// Ответ на получение роли пользователя
/// </summary>
public class GetUserRoleResponse
{
    /// <summary>
    /// роль
    /// </summary>
    public required string Role { get; init; }
}