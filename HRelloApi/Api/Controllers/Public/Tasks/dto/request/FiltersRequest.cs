using Dal.Tasks.Enum;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.Tasks.dto.request;

/// <summary>
/// Модель данных для запроса получения всех задач с учетов фильтров
/// </summary>
public class FiltersRequest
{
    public string? Year { get; init; }
    public string? Quarter { get; init; }
    public string? Block { get; init; }
    
    [JsonProperty("User")]
    public string? User { get; init; }
    public string? DepartmentId { get; init; }
}