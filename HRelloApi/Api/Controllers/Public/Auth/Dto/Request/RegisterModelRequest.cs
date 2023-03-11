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
    public string Name { get; init; }
    
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    [Required]
    [JsonProperty("Surname")]
    public string Surname { get; init; }
    
    /// <summary>
    /// Отчество пользователя
    /// </summary>
    [Required]
    [JsonProperty("Patronymic")]
    public string Patronymic { get; init; }
    
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    [JsonProperty("Password")]
    public string Password { get; init; }
}