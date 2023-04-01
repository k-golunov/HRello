using Dal.Tasks.Entities;
using Dal.Tasks.Enum;
using Dal.Tasks.Repositories.Interfaces;
using Logic.Managers.Base;
using Logic.Managers.Tasks.Interfaces;
using Logic.Managers.Tasks.StatusesTree;

namespace Logic.Managers.Tasks;

public class StatusManager: BaseManager<TaskDal, Guid>, ITaskStatusManager
{
    private StatusTree _statusTree;
    public StatusManager(ITaskRepository repository, StatusTree statusTree) : base(repository)
    {
        _statusTree = statusTree;
    }
    
    public void ChangeStatus(TaskDal task, StatusEnum nextStatus)
    {
        var statusNode = _statusTree.GetStatusNode(task.Status);
        if (statusNode.IsNextStatus(nextStatus))
        {
            task.Status = nextStatus;
        }
        else
        {
            throw new Exception();//?????Может какую-то другую обработку
        }
    }
}