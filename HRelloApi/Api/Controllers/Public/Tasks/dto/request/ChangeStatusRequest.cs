using System.ComponentModel.DataAnnotations;
using Dal.Tasks.Enum;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.Tasks.dto.request;

/// <summary>
/// Модель запроса на изменение статуса задачи
/// </summary>
public class ChangeStatusRequest
{
    /// <summary>
    /// Id задачи
    /// </summary>
    [Required]
    [JsonProperty("Id")]
    public required Guid Id { get; init; }
    
    /// <summary>
    /// Новый статус
    /// </summary>
    [Required]
    [JsonProperty("NextStatus")]
    [Range(0,6)]
    public required StatusEnum NextStatus { get; init; }
    
    /// <summary>
    /// Комментарий к изменению статуса
    /// </summary>
    [JsonProperty("Comment")]
    public string? Comment { get; init; }
}