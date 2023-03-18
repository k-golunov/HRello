using System.ComponentModel.DataAnnotations;

namespace HRelloApi.Controllers.Public.Departament.Dto.Request;

/// <summary>
/// Создание нового отдела
/// </summary>
public record CreateDepartamentRequest
{
    /// <summary>
    /// Название отдела
    /// </summary>
    [Required]
    public required string Name { get; init; }
    
    /// <summary>
    /// Id начальника отдела
    /// </summary>
    [Required]
    public required Guid BossId { get; init; }
}