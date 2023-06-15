namespace HRelloApi.Controllers.Public.Departament.Dto.Response;

/// <summary>
/// Возвращаемые значение после создания отдела
/// </summary>
public record IdDepartmentResponse
{
    /// <summary>
    /// Id отдела
    /// </summary>
    public int Id { get; init; }
}