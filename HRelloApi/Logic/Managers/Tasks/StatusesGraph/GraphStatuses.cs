using Dal.Tasks.Enum;

namespace Logic.Managers.Tasks.StatusesGraph;

public static class StatusesGraph
{
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

    public static ActionTypeEnum GetAction(StatusEnum statusFrom, StatusEnum statusTo)
    {
        return matrix[(int)statusFrom, (int)statusTo];
    }
}
