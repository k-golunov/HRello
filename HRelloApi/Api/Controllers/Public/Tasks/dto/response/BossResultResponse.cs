using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.Tasks.dto.response;

/// <summary>
/// 
/// </summary>
public record BossResultResponse
{
    /// <summary>
    /// Id итога руководителя
    /// </summary>
    [Required]
    [JsonProperty("Id")]
    public Guid Id { get; init; }
    /// <summary>
    /// Результат от руководителя в процентах
    /// </summary>
    [Required]
    [JsonProperty("Result")]
    public int Result { get; init; }
    
    /// <summary>
    /// Комментарий к исполнению задачи от руководителя
    /// </summary>
    [Required]
    [JsonProperty("Comment")]
    public string? Comment { get; init; }
}