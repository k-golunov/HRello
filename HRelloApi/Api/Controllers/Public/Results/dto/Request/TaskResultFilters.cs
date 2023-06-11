using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.Results.dto.Request;

/// <summary>
/// фильтры для итогов
/// </summary>
public record TaskResultFilters
{
    /// <summary>
    /// год итогов
    /// </summary>
    [Required]
    [JsonProperty("Year")]
    public required int Year { get; init; }
    
    /// <summary>
    /// кварталы итогов
    /// </summary>
    [Required]
    [JsonProperty("Quarters")]
    public required string Quarters { get; init; }
    
    /// <summary>
    /// отделы для итогов
    /// </summary>
    [Required]
    [JsonProperty("DepartmentsId")]
    public required string DepartmentsId { get; init; }
}