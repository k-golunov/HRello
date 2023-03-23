using System.ComponentModel.DataAnnotations;

namespace HRelloApi.Controllers.Public.Task.dto.request;

/// <summary>
/// Входная модель данных для создания сущности задачи
/// </summary>
public class CreateTaskRequest
{
    /// <summary>
    /// Id сотрудника, создавшего задачу
    /// </summary>
    [Required]
    public required Guid UserId { get; init; }
}