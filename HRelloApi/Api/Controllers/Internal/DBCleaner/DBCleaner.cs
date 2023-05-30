using Dal.Tasks.Repositories.Interfaces;
using Logic.Managers.Tasks.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRelloApi.Controllers.Internal.DBCleaner;

/// <summary>
/// очищает данные в бд
/// </summary>
[Route("api/v1/internal/clean")]
public class DBCleaner : ControllerBase
{
    private readonly ITaskRepository _taskRepository;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="taskRepository"></param>
    public DBCleaner(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpDelete("delete-all-tasks")]
    public async Task<IActionResult> CleanTasksTable()
    {
        await _taskRepository.DeleteAll();
        return Ok();
    }
}