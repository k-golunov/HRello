using Dal.Base.Interfaces;
using Dal.Results.Repositories.Interfaces;
using Dal.TaskResult.Entities;
using Dal.Tasks.Entities;
using Logic.Exceptions.TaskResultException;
using Logic.Managers.Base;
using Logic.Managers.Result.Interfaces;

namespace Logic.Managers.Result;

public class ResultManager : BaseManager<TaskResultDal, Guid>, IResultManager
{
    public ResultManager(IResultRepository repository) : base(repository)
    {
    }

    public void ValidationData(List<TaskDal> tasks, int year, int quarter)
    {
        var tasksValid = tasks.Where(t => t.Year == year && t.Quarter == quarter).ToList();
        if (tasksValid.Count != tasks.Count)
        {
            throw new NotValidYearOrQuarterException(year, quarter);
        }
    }

    public Task<byte[]> GenerateFileAsync()
    {
        throw new NotImplementedException();
    }
}