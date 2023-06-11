namespace HRelloApi.Controllers.Public.Tasks.dto.Base;

/// <summary>
/// Базовая модель для запроса к задаче
/// </summary>
public abstract record BaseRequestModel
{
    /// <summary>
    /// Идентификатор для задачи, пользователь, который внес изменения
    /// </summary>
    public required string ChangeByUserId { get; init; }
}