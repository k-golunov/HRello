using Microsoft.AspNetCore.Identity;

namespace HRelloApi.Controllers.Internal.Role.Dto.Request;

/// <summary>
/// Модель для создания ролей в идентити
/// </summary>
public record CreateRoleInternalRequest
{
    /// <summary>
    /// Название роли
    /// </summary>
    public required IdentityRole RoleName { get; init; }
}