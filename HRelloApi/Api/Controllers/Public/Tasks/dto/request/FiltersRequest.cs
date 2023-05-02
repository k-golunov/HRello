using System.ComponentModel.DataAnnotations;
using Dal.Tasks.Enum;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.Tasks.dto.request;

/// <summary>
/// Модель данных для запроса получения всех задач с учетов фильтров
/// </summary>
public class FiltersRequest
{
    [Required]
    [JsonProperty("Year")]
    public required string? Year { get; init; }
    [Required]
    [JsonProperty("Quarter")]
    public required string? Quarter { get; init; }
    [Required]
    [JsonProperty("Block")]
    public required string? Block { get; init; }
    [JsonProperty("User")]
    public required string? User { get; init; }
    [Required]
    [JsonProperty("DepartmentId")]
    public required string? DepartmentId { get; init; }
}