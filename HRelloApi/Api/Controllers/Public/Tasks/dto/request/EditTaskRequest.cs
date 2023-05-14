using System.ComponentModel.DataAnnotations;
using Dal.Tasks.Enum;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.Tasks.dto.request;

/// <summary>
/// Входная модель данных для реста редактирования данных задачи
/// </summary>
public record EditTaskRequest
{
    /// <summary>
    /// Идентификатор задачи
    /// </summary>
    public required Guid Id { get; init; } 
    
    /// <summary>
    /// Название задачи
    /// </summary>
    [Required]
    [JsonProperty("Name")]
    public required string Name { get; init; }

    /// <summary>
    /// Текущий год
    /// </summary>
    [Required]
    [JsonProperty("Year")]
    public required int Year { get; init; }

    /// <summary>
    /// Квартал
    /// </summary>
    [Required]
    [JsonProperty("Quarter")]
    public required int Quarter { get; init; }

    /// <summary>
    /// Категория задачи
    /// </summary>
    [Required]
    [JsonProperty("Category")]
    [Range(0,1)]
    public required CategoryEnum Category { get; init; }

    /// <summary>
    /// Блок
    /// </summary>
    [Required]
    [JsonProperty("Block")]
    public required Guid BlockId { get; init; }

    /// <summary>
    /// Планируемый вес задачи
    /// </summary>
    [Required]
    [JsonProperty("PlannedWeight")]
    public required int PlannedWeight { get; init; }
    
    /// <summary>
    /// Ожидаемый результат
    /// </summary>
    [Required]
    [JsonProperty("WaitResult")]
    public required string WaitResult { get; init; }
    
}
