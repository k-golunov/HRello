namespace HRelloApi.Controllers.Public.Departament.Dto.Response;

/// <summary>
/// Возвращаемые значение после создания отдела
/// </summary>
public record CreateIdResponse
{
    /// <summary>
    /// Id отдела
    /// </summary>
    public int Id { get; init; }
}