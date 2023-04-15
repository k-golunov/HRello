using System.ComponentModel.DataAnnotations;
using Dal.Tasks.Entities;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.Task.dto.request;

/// <summary>
/// Модель запроса на завершение задачи сотрудником
/// </summary>
public class UserTaskCompletedRequest
{
    /// <summary>
    /// Фактический результат, в процентах
    /// </summary>
    [Required]
    [JsonProperty("FactResult")]
    [Range(0, 1000)]
    public required int FactResult { get; init; }
    
    /// <summary>
    /// Описание достигнутого результата
    /// </summary>
    [Required]
    [JsonProperty("Result")]
    [MaxLength(1023)]
    public required string Result { get; init; }
    
    /// <summary>
    /// Комментарий от сотрудника к завершению задачи
    /// </summary>
    [Required]
    [JsonProperty("Description")]
    [MaxLength(255)]
    public required string? Description { get; init; }
    
    /// <summary>
    /// Фактический вес
    /// </summary>
    [Required]
    [JsonProperty("FactWeight")]
    [Range(0, 1000)]
    public required int FactWeight { get; init; }
    
    /// <summary>
    /// Фактический вес
    /// </summary>
    [Required]
    [JsonProperty("TaskId")]
    public required Guid TaskId { get; init; }
}