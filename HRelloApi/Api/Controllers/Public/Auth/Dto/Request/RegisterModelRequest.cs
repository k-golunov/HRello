using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.Auth.Dto.Request;

/// <summary>
/// Модель данных при регистрации пользователя
/// </summary>
public record RegisterModelRequest
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    [Required]
    [JsonProperty("Name")]
    public required string Name { get; init; }
    
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    [Required]
    [JsonProperty("Surname")]
    public required string Surname { get; init; }
    
    /// <summary>
    /// Отчество пользователя
    /// </summary>
    [JsonProperty("Patronymic")]
    public string? Patronymic { get; init; }
    
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    [JsonProperty("Password")]
    public required string Password { get; init; }
}