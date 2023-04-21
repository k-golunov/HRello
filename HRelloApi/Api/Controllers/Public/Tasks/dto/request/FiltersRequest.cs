using Dal.Tasks.Enum;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.Tasks.dto.request;

/// <summary>
/// Модель данных для запроса получения всех задач с учетов фильтров
/// </summary>
public class FiltersRequest
{
    public int? Year { get; init; }
    public int? Quarter { get; init; }
    public BlockEnum? Block { get; init; }
    
    [JsonProperty("User")]
    public Guid? User { get; init; }
    public int? DepartmentID { get; init; }
}