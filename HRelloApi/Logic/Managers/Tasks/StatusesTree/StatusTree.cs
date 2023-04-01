using Dal.Tasks.Enum;

namespace Logic.Managers.Tasks.StatusesTree;

public class StatusTree
{
    public static List<StatusNode> Statuses = new ();

    public StatusTree()
    {
        var forRevision = new StatusNode(StatusEnum.OnChecking);
        var underReview = new StatusNode(StatusEnum.UnderReview);
        var inWork = new StatusNode(StatusEnum.InWork);
        var completionCheck = new StatusNode(StatusEnum.CompletionCheck);
        var completed = new StatusNode(StatusEnum.Completed);
        var awaitingCancellation = new StatusNode(StatusEnum.AwaitingCancellation);
        var canceled = new StatusNode(StatusEnum.Canceled);
        
        forRevision.AddNextStatus(new List<StatusNode>{ underReview, inWork, canceled });
        underReview.AddNextStatus(new List<StatusNode> { forRevision });
        inWork.AddNextStatus(new List<StatusNode> { awaitingCancellation, completionCheck });
        completionCheck.AddNextStatus(new List<StatusNode> { canceled, completed, inWork });
        completed.AddNextStatus(new List<StatusNode>());
        awaitingCancellation.AddNextStatus(new List<StatusNode> { canceled, inWork });
        canceled.AddNextStatus(new List<StatusNode>());
        
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