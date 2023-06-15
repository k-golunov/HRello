using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.Departament.Dto.Request;

/// <summary>
/// редиктирование отдела
/// </summary>
public record EditDepartmentRequest
{
    /// <summary>
    /// идентификатор отдела
    /// </summary>
    [JsonProperty("DepartamentId")]  
    [Required]
    public required int DepartamentId { get; init; }
    
    /// <summary>
    /// навзание отдела
    /// </summary>
    [JsonProperty("Name")]  
    public string? Name { get; init; }
    
    /// <summary>
    /// Id начальника отдела
    /// </summary>
    public string? BossId { get; init; }
}