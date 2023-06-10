namespace HRelloApi.Controllers.Public.Departament.Dto.Request;

/// <summary>
/// обновление отдела сотрудника
/// </summary>
public record UpdateUserDepartmentRequest
{
    /// <summary>
    /// идентификатор пользоавателя
    /// </summary>
    public required string UserId { get; init; }
    
    /// <summary>
    /// Идентификатор нового отдела
    /// </summary>
    public required int NewDepartmentId { get; init; }
}