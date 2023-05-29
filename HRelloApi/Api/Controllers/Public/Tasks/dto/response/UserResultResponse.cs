using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.Tasks.dto.response;

/// <summary>
/// Вывод итогов сотрудника
/// </summary>
public record UserResultResponse
{
    [Required]
    [JsonProperty("Id")]
    public Guid Id { get; init; }
    /// <summary>
    /// Фактический результат, в процентах
    /// </summary>
    [Required]
    [JsonProperty("FactResult")]
    public int FactResult { get; init; }
    
    /// <summary>
    /// Описание достигнутого результата
    /// </summary>
    [Required]
    [JsonProperty("Result")]
    public string Result { get; init; }
    
    /// <summary>
    /// Комментарий от сотрудника к завершению задачи
    /// </summary>
    [Required]
    [JsonProperty("Description")]
    public string? Description { get; init; }
    
    /// <summary>
    /// Фактический вес
    /// </summary>
    [Required]
    [JsonProperty("FactWeight")]
    public int FactWeight { get; init; }
}