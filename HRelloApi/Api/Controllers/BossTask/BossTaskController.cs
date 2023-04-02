using HRelloApi.Controllers.Public.Base;
using Logic.Managers.Tasks.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRelloApi.Controllers.BossTask;

/// <summary>
/// 
/// </summary>
public class BossTaskController : BasePublicController
{
    private readonly ITaskUnitOfWorkManager _manager;

    public BossTaskController(ITaskUnitOfWorkManager manager)
    {
        _manager = manager;
    }

    [HttpPatch]
    public async Task<IActionResult> ChangeStatus()
    {
        return Ok();
    }
}