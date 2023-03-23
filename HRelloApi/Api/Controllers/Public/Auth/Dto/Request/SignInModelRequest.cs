using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.Auth.Dto.Request;

/// <summary>
/// Модель данных для запроса авторизации пользователя
/// </summary>
public record SignInModelRequest
{
    /// <summary>
    /// Электронная почта авторизируемого пользователя
    /// </summary>
    [Required]
    [EmailAddress]
    [JsonProperty("Email")]
    public required string Email { get; init; }
    
    /// <summary>
    /// Пароль авторизируемого пользователя
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    [JsonProperty("Password")]
    public required string Password { get; init; }
    
    /// <summary>
    /// Запомнить пароль?
    /// </summary>
    [Required]
    [DefaultValue(false)]
    [JsonProperty("RememberMe")]
    public required bool RememberMe { get; init; }
}