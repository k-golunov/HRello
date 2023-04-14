using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Internal.Role.Dto.Response;

/// <summary>
/// Модель со всеми ролями
/// </summary>
public record AllRolesResponse()
{
    /// <summary>
    /// Роли
    /// </summary>
    [JsonProperty("roles")]
    [Required]
    public required List<IdentityRole> Roles { get; init; }
}