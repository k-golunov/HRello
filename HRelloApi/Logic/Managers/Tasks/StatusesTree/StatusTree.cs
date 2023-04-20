using Dal.Tasks.Enum;

namespace Logic.Managers.Tasks.StatusesTree;

public class StatusTree
{
    public static List<StatusNode> Statuses = new ();

    public StatusTree()
    {
        var forRevision = new StatusNode(StatusEnum.OnChecking);
        var underReview = new StatusNode(StatusEnum.OnRework);
        var inWork = new StatusNode(StatusEnum.InWork);
        var completionCheck = new StatusNode(StatusEnum.CompletionCheck);
        var completed = new StatusNode(StatusEnum.Completed);
        var awaitingCancellation = new StatusNode(StatusEnum.AwaitingCancellation);
        var canceled = new StatusNode(StatusEnum.Canceled);
        
        forRevision.AddNextStatus(new Dictionary<StatusNode, ActionTypeEnum>
        {
            {underReview, ActionTypeEnum.OnRework},
            {inWork, ActionTypeEnum.OnWork},
            {canceled, ActionTypeEnum.Cancellation}
        });
        underReview.AddNextStatus(new Dictionary<StatusNode, ActionTypeEnum>
        {
            { forRevision, ActionTypeEnum.OnChecking },
        });
        inWork.AddNextStatus(new Dictionary<StatusNode, ActionTypeEnum> 
        {
            { awaitingCancellation, ActionTypeEnum.OnCancellation},
            { completionCheck, ActionTypeEnum.OnCompletion }
        });
        completionCheck.AddNextStatus(new Dictionary<StatusNode, ActionTypeEnum>
        {
            {canceled, ActionTypeEnum.Cancellation},
            {completed, ActionTypeEnum.Completion},
            {inWork, ActionTypeEnum.CompletionDeviation}
        });
        completed.AddNextStatus(new Dictionary<StatusNode, ActionTypeEnum>());
        awaitingCancellation.AddNextStatus(new Dictionary<StatusNode, ActionTypeEnum>
        {
            {canceled, ActionTypeEnum.Cancellation},
            {inWork, ActionTypeEnum.CancellationDeviation}
        });
        canceled.AddNextStatus(new Dictionary<StatusNode, ActionTypeEnum>());
        
        Statuses.Add(forRevision);
        Statuses.Add(underReview);
        Statuses.Add(inWork);
        Statuses.Add(completionCheck);
        Statuses.Add(completed);
        Statuses.Add(awaitingCancellation);
        Statuses.Add(canceled);
    }

    public StatusNode GetStatusNode(StatusEnum status)
    {
        return Statuses.First(x => x.Status == status);
    }
    
    
}