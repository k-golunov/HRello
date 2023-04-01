namespace HRelloApi.Controllers.Base.Exception;

/// <summary>
/// Модель для отправки ошибки в контроллере
/// </summary>
public class BaseExceptionModel
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public required string Code { get; set; }
    /// <summary>
    /// Сообщение ошибки
    /// </summary>
    public required string Message { get; set; }
}