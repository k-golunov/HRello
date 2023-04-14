namespace HRelloApi.Controllers.Base.Exception;

/// <summary>
/// Модель для отправки ошибки в контроллере
/// </summary>
public record BaseExceptionModel(string Code, string Message)
{
    /// <summary>
    /// Код ошибки
    /// </summary>
    public string Code { get; set; } = Code;

    /// <summary>
    /// Сообщение ошибки
    /// </summary>
    public string Message { get; set; } = Message;
}