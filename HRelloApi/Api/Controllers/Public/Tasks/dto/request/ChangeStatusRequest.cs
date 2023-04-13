using System.ComponentModel.DataAnnotations;
using Dal.Tasks.Enum;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.Task.dto.request;

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
    public Guid Id { get; init; }
    
    /// <summary>
    /// Новый статус
    /// </summary>
    [Required]
    [JsonProperty("NextStatus")]
    public StatusEnum NextStatus { get; init; }
}