using Dal.Tasks.Enum;

namespace Logic.Managers.Task.StatusesTree;

public class StatusNode
{
    public Status Status { get; set; }

    public List<StatusNode> NextStatuses { get; set; }

    public StatusNode(Status status)
    {
        Status = status;
        NextStatuses = new List<StatusNode>();
    }

    public void AddNextStatus(List<StatusNode> nextStatuses)
    {
        NextStatuses = nextStatuses;
    }

    public bool IsNextStatus(Status status)
    {
        return NextStatuses.Select(x => x.Status).Contains(status);
    }
}