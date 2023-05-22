namespace Logic.Managers.Tasks.Filters;

public class FilteredTask
{
    public Guid Id;
    /// <summary>
    /// Фильтр по году
    /// </summary>
    public string? Year { get; init; } 
    /// <summary>
    /// Фильтр по кварталу
    /// </summary>
    public string? Quarter { get; init; }
    /// <summary>
    /// Фильтр по блоку
    /// </summary>
    public string? BlockId { get; init; }
    /// <summary>
    /// Фильтр по сотруднику
    /// </summary>
    public string? UserId { get; init; }
    /// <summary>
    /// Фильтр по департаменту
    /// </summary>
    public string? DepartmentId { get; init; } 
}