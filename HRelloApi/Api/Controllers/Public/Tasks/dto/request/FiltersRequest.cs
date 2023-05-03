using System.ComponentModel.DataAnnotations;
using Dal.Tasks.Enum;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.Tasks.dto.request;

/// <summary>
/// Модель данных для запроса получения всех задач с учетов фильтров
/// </summary>
public class FiltersRequest
{
    [JsonProperty("Year")]
    public string? Year { get; init; }
    [JsonProperty("Quarter")]
    public string? Quarter { get; init; }
    [JsonProperty("Block")]
    public string? Block { get; init; }
    [JsonProperty("User")]
    public string? User { get; init; }
    [JsonProperty("DepartmentId")]
    public string? DepartmentId { get; init; }
}