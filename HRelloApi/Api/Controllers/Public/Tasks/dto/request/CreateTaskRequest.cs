using System.ComponentModel.DataAnnotations;
using Dal.Tasks.Enum;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.Tasks.dto.request;

/// <summary>
/// Входная модель данных для создания сущности задачи
/// </summary>
public record CreateTaskRequest
{
    /// <summary>
    /// Название задачи
    /// </summary>
    [Required]
    [JsonProperty("Name")]
    public required string Name { get; init; }
    
    /// <summary>
    /// Год создания задачи
    /// </summary>
    [Required]
    [JsonProperty("Year")]
    public required int Year { get; init; }
    
    /// <summary>
    /// Квартал задачи
    /// </summary>
    [Range(1,4)]
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
    /// Блок задачи
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