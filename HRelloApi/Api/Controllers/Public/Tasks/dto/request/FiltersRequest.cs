using System.ComponentModel.DataAnnotations;
using Dal.Tasks.Enum;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.Tasks.dto.request;

/// <summary>
/// Модель данных для запроса получения всех задач с учетов фильтров
/// </summary>
public class FiltersRequest
{
    /// <summary>
    /// Фильтр по году
    /// </summary>
    [JsonProperty("Year")]
    public string? Year { get; init; }
    
    /// <summary>
    /// Фильтр по кварталу
    /// </summary>
    [JsonProperty("Quarter")]
    public string? Quarter { get; init; }
    
    /// <summary>
    /// Фильтр по блоку задач
    /// </summary>
    [JsonProperty("Block")]
    public string? Block { get; init; }
    
    /// <summary>
    /// Фильтр по сотруднику
    /// </summary>
    [JsonProperty("User")]
    public string? User { get; init; }
    
    /// <summary>
    /// Фильтр по департаменту
    /// </summary>
    [JsonProperty("DepartmentId")]
    public string? DepartmentId { get; init; }
    /// <summary>
    /// Фильтр по статусу
    /// </summary>
    [JsonProperty("Status")]
    public string? Status { get; init; }
}