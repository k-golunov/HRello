using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.Tasks.dto.response;

/// <summary>
/// 
/// </summary>
public class TaskResultResponse
{
    /// <summary>
    /// Id созданных сотрдуником итогов по задаче 
    /// </summary>
    [Required]
    [JsonProperty("ResultId")]
    public required Guid ResultId { get; init; }
    
    /// <summary>
    /// Id задачи
    /// </summary>
    [Required]
    [JsonProperty("TaskId")]
    public required Guid TaskId { get; init; }
}