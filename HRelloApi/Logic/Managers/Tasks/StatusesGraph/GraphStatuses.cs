using Dal.Tasks.Enum;

namespace Logic.Managers.Tasks.StatusesGraph;

/// <summary>
/// класс, реализующий взвешенный граф
/// где вершины графа - статусы задач
/// вес перехода между вершинами - действие, совершенное для перехода, записываемое в историю изменения задачи
/// </summary>
public static class StatusesGraph
{
    /// <summary>
    /// матрица, описывающая взвешенный граф
    /// </summary>
    private static readonly ActionTypeEnum[,] matrix = 
    {
        { ActionTypeEnum.OnChecking, ActionTypeEnum.OnChecking, ActionTypeEnum.OnRework, ActionTypeEnum.OnWork, ActionTypeEnum.None, ActionTypeEnum.None, ActionTypeEnum.None },
        { ActionTypeEnum.OnChecking, ActionTypeEnum.None, ActionTypeEnum.None, ActionTypeEnum.None, ActionTypeEnum.None, ActionTypeEnum.None, ActionTypeEnum.None },
        { ActionTypeEnum.None, ActionTypeEnum.None, ActionTypeEnum.None, ActionTypeEnum.OnCompletion, ActionTypeEnum.None, ActionTypeEnum.OnCancellation, ActionTypeEnum.None },
        { ActionTypeEnum.None, ActionTypeEnum.None, ActionTypeEnum.CompletionDeviation, ActionTypeEnum.None, ActionTypeEnum.Completion, ActionTypeEnum.None, ActionTypeEnum.Cancellation },
        { ActionTypeEnum.None, ActionTypeEnum.None, ActionTypeEnum.None, ActionTypeEnum.None, ActionTypeEnum.None, ActionTypeEnum.None, ActionTypeEnum.None },
        { ActionTypeEnum.None, ActionTypeEnum.None, ActionTypeEnum.CancellationDeviation, ActionTypeEnum.None, ActionTypeEnum.None, ActionTypeEnum.None, ActionTypeEnum.Cancellation },
        { ActionTypeEnum.None, ActionTypeEnum.None, ActionTypeEnum.None, ActionTypeEnum.None, ActionTypeEnum.None, ActionTypeEnum.None, ActionTypeEnum.None },
    };

    /// <summary>
    /// Возвращает соответвующий элемент матрицы по входящим индексам
    /// </summary>
    public static ActionTypeEnum GetAction(StatusEnum statusFrom, StatusEnum statusTo)
    {
        return matrix[(int)statusFrom, (int)statusTo];
    }
}
