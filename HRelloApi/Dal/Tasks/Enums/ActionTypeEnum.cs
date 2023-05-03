namespace Dal.Tasks.Enum;

public enum ActionTypeEnum
{
    None = 10,
    /// <summary>
    /// Отправлена на проверку
    /// </summary>
    OnChecking = 0,
    
    /// <summary>
    /// Отправлена на доработку
    /// </summary>
    OnRework = 1,
    
    /// <summary>
    /// Отправлена в работу
    /// </summary>
    OnWork = 2,
    
    /// <summary>
    /// Отправлена на отмену
    /// </summary>
    OnCancellation = 3,
    
    /// <summary>
    /// Отменена
    /// </summary>
    Cancellation = 4,
    
    /// <summary>
    /// Отмена отклонения
    /// </summary>
    CancellationDeviation = 5,
    
    /// <summary>
    /// Отправлена на завершение
    /// </summary>
    OnCompletion = 6,
    
    /// <summary>
    /// Завершение отклоненно
    /// </summary>
    CompletionDeviation = 7,
    
    /// <summary>
    /// Завершена
    /// </summary>
    Completion = 8,
    
    /// <summary>
    /// Обновлена
    /// </summary>
    Updated = 9,
}