using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.Auth.Dto.Request;

/// <summary>
/// Модель данных для создания нового пользователя
/// </summary>
public record CreateUserModelRequest
{
    /// <summary>
    /// Электронная почта создавамого пользователя
    /// </summary>
    [Required]
    [EmailAddress]
    [JsonProperty("Email")]
    public string Email { get; init; }

    /// <summary>
    /// Роль создаваемого пользователя
    /// </summary>
    [Required]
    [JsonProperty("Role")]
    public string Role { get; init; }
    
    /// <summary>
    /// Id отдела создаваемого пользователя
    /// </summary>
    [Required]
    [JsonProperty("DepartmentId")]
    public int DepartamentId { get; init; }
}