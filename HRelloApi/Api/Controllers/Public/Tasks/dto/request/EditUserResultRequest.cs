using System.ComponentModel.DataAnnotations;
using HRelloApi.Controllers.Public.Tasks.dto.Base;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.Tasks.dto.request;

/// <summary>
/// редактирование итогов сотрудника
/// </summary>
public record EditUserResultRequest: BaseRequestModel
{
    /// <summary>
    /// Id 
    /// </summary>
    [Required]
    [JsonProperty("Id")]
    public Guid Id { get; init; }
    
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