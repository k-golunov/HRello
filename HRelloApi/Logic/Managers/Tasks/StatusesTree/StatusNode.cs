using Dal.Tasks.Enum;

namespace Logic.Managers.Tasks.StatusesTree;

public class StatusNode
{
    public StatusEnum Status { get; set; }

    public Dictionary<StatusNode, ActionTypeEnum> NextStatuses { get; set; }

    public StatusNode(StatusEnum status)
    {
        Status = status;
        NextStatuses = new Dictionary<StatusNode, ActionTypeEnum>();
    }

    public void AddNextStatus(Dictionary<StatusNode, ActionTypeEnum> nextStatuses)
    {
        NextStatuses = nextStatuses;
    }

    public bool IsNextStatus(StatusEnum status)
    {
        return NextStatuses.Select(x => x.Key.Status).Contains(status);
    }

    public ActionTypeEnum GetAction(StatusEnum status)
    {
        var key = NextStatuses.Keys.First(x => x.Status == status);
        return NextStatuses[key];
    }
}