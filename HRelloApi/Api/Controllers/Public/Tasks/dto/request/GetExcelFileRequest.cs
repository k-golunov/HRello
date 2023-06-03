namespace HRelloApi.Controllers.Public.Tasks.dto.request;

public record GetExcelFileRequest
{
    /// <summary>
    /// Год задач
    /// </summary>
    public required int Year { get; init; }
    
    /// <summary>
    /// Кварталы задач
    /// </summary>
    public required List<int> Quearter { get; init; }
}