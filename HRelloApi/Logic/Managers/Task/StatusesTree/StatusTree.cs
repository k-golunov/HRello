using Dal.Tasks.Enum;

namespace Logic.Managers.Task.StatusesTree;

public class StatusTree
{
    public static List<StatusNode> Statuses = new ();

    public StatusTree()
    {
        var forRevision = new StatusNode(Status.ForRevision);
        var underReview = new StatusNode(Status.UnderReview);
        var inWork = new StatusNode(Status.InWork);
        var completionCheck = new StatusNode(Status.CompletionCheck);
        var completed = new StatusNode(Status.Completed);
        var awaitingCancellation = new StatusNode(Status.AwaitingCancellation);
        var canceled = new StatusNode(Status.Canceled);
        
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

    public StatusNode GetStatusNode(Status status)
    {
        return Statuses.First(x => x.Status == status);
    }
    
}