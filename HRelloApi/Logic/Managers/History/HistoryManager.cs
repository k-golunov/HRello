using Dal.Base.Interfaces;
using Dal.Tasks.Entities;
using Dal.Tasks.Repositories.Interfaces;
using Logic.Managers.Base;
using Logic.Managers.History.Interfaces;

namespace Logic.Managers.History;

public class HistoryManager : IHistoryManager
{
    private readonly IHistoryRepository _repository;
    
    public HistoryManager(IHistoryRepository repository)
    {
        _repository = repository;
    }

    public List<HistoryDal> GetAllHistoryByTaskId(Guid taskId)
    {
        return _repository.GetAllHistoryByTaskId(taskId);
    }
}