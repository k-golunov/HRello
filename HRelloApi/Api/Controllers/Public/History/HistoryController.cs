using HRelloApi.Controllers.Public.Base;
using HRelloApi.Controllers.Public.History.dto.Response;
using Logic.Managers.History.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRelloApi.Controllers.Public.History;

/// <summary>
/// Контроллер для истории
/// </summary>
public class HistoryController : BasePublicController
{
    private readonly IHistoryManager _historyManager;
    
    public HistoryController(IHistoryManager historyManager)
    {
        _historyManager = historyManager;
    }

    /// <summary>
    /// Получение всей истории по айди задачи
    /// </summary>
    /// <param name="taskId"></param>
    /// <returns></returns>
    [HttpGet("{taskId:guid}")]
    public IActionResult GetAllHistoryByTaskId([FromRoute] Guid taskId)
    {
        var allHistory = _historyManager.GetAllHistoryByTaskId(taskId);
        return Ok(new AllHistoryByTaskIdResponse
        {
            AllHistory = allHistory
        });
    }
}