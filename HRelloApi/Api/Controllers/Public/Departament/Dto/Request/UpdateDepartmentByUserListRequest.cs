namespace HRelloApi.Controllers.Public.Departament.Dto.Request;

/// <summary>
/// прикреплении нескольки пользователей к отделу
/// </summary>
public record UpdateDepartmentByUserListRequest
{
    /// <summary>
    /// идентификатор пользоавателя
    /// </summary>
    public required List<string> UserIdList { get; init; }
    
    /// <summary>
    /// Идентификатор нового отдела
    /// </summary>
    public required int NewDepartmentId { get; init; }
}