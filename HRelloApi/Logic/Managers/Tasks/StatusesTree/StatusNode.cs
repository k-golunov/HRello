using Dal.Tasks.Enum;

namespace Logic.Managers.Tasks.StatusesTree;

public class StatusNode
{
    public StatusEnum Status { get; set; }

    public List<StatusNode> NextStatuses { get; set; }

    public StatusNode(StatusEnum status)
    {
        Status = status;
        NextStatuses = new List<StatusNode>();
    }

    public void AddNextStatus(List<StatusNode> nextStatuses)
    {
        NextStatuses = nextStatuses;
    }

    public bool IsNextStatus(StatusEnum status)
    {
        return NextStatuses.Select(x => x.Status).Contains(status);
    }
}