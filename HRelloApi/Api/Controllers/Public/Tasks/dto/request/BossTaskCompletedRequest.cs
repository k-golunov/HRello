using System.ComponentModel.DataAnnotations;
using HRelloApi.Controllers.Public.Tasks.dto.Base;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.Tasks.dto.request;

/// <summary>
/// Модель запроса на полное завершение задачи
/// </summary>
public record BossTaskCompletedRequest : BaseRequestModel
{
    /// <summary>
    /// Результат от руководителя в процентах
    /// </summary>
    [Required]
    [JsonProperty("Result")]
    [Range(0,1000)]
    public required int Result { get; init; }
    
    /// <summary>
    /// Комментарий к исполнению задачи от руководителя
    /// </summary>
    [Required]
    [JsonProperty("Comment")]
    [MaxLength(255)]
    public required string Comment { get; init; }
    
    /// <summary>
    /// Идентификатор задачи
    /// </summary>
    [Required]
    [JsonProperty("TaskId")]
    public required Guid TaskId { get; init; }
}