using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.Task.dto.request;

/// <summary>
/// Входная модель данных для создания сущности задачи
/// </summary>
public class CreateTaskRequest
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
    public required string Category { get; init; }
    
    /// <summary>
    /// Блок задачи
    /// </summary>
    [Required]
    [JsonProperty("Block")]
    public required string Block { get; init; }
    
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
    
    /// <summary>
    /// Id сотрудника, создавшего задачу
    /// </summary>
    [Required]
    [JsonProperty("UserId")]
    public required Guid UserId { get; init; }
}