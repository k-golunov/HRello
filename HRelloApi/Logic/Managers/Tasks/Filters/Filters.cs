namespace Logic.Managers.Tasks.Filters;

/// <summary>
/// Класс отвечающий за фильтры
/// Содержит поля, по которым возможна фильтрация задач
/// </summary>
public class Filters
{ 
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
    public string? Block { get; init; }
    /// <summary>
    /// Фильтр по сотруднику
    /// </summary>
    public string? User { get; init; }
    /// <summary>
    /// Фильтр по департаменту
    /// </summary>
    public string? DepartmentId { get; init; } 
}
