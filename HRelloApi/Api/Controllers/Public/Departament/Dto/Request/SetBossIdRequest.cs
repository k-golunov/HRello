using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace HRelloApi.Controllers.Public.Departament.Dto.Request;

/// <summary>
/// Модель для прикрепления отдела к руководителю
/// </summary>
public record SetBossIdRequest
{
    /// <summary>
    /// идентификатор отдела
    /// </summary>
    [JsonProperty("departamentId")]  
    [Required]
    public required int DepartamentId { get; init; }
    
    /// <summary>
    /// Id начальника отдела
    /// </summary>
    [Required]
    public required string BossId { get; init; }
}