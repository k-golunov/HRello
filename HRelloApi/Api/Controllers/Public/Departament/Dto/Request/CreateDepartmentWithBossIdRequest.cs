using System.ComponentModel.DataAnnotations;

namespace HRelloApi.Controllers.Public.Departament.Dto.Request;

/// <summary>
/// Создание отдела с босс айди
/// </summary>
public class CreateDepartmentWithBossIdRequest
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