using Dal.Tasks.Entities;
using HRelloApi.Controllers.Public.Base;
using HRelloApi.Controllers.Public.Block.dto.request;
using HRelloApi.Controllers.Public.Block.dto.response;
using Logic.Exceptions.Tasks;
using Logic.Managers.Tasks.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRelloApi.Controllers.Public.Block;

/// <summary>
/// 
/// </summary>
public class BlockController: BasePublicController
{
    private readonly ITaskUnitOfWorkManager _manager;

    public BlockController(ITaskUnitOfWorkManager manager)
    {
        _manager = manager;
    }

    /// <summary>
    /// рест на создание блока задач
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(BlockResponse), 200)]
    public async Task<IActionResult> CreateBlockAsync(CreateBlockRequest request)
    {
        var block = new BlockDal(request.Value);
        var id = await _manager.InsertAsync(block);
        return Ok(new BlockResponse(id, block.Value));
    }

    /// <summary>
    /// рест на получение блока задачи
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BlockResponse), 200)]
    public async Task<IActionResult> GetBlockAsync([FromRoute] Guid id)
    {
        var block = await _manager.GetAsync<BlockDal>(id);
        if (block == null)
            throw new BlockNotFoundException();
        return Ok(new BlockResponse(block.Id, block.Value));
    }

    /// <summary>
    /// рест на обновление блока задач
    /// </summary>
    [HttpPut]
    [ProducesResponseType(typeof(BlockResponse), 200)]
    public async Task<IActionResult> UpdateBlockAsync([FromBody] UpdateBlockRequest request)
    {
        var block = await _manager.GetAsync<BlockDal>(request.Id);
        if (block == null)
            throw new BlockNotFoundException();
        block.Value = request.Value;
        var id = _manager.UpdateAsync(block);
        return Ok(new BlockResponse(block.Id, block.Value));
    }

    /// <summary>
    /// рест на удаление блока задач
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteBlockAsync([FromRoute] Guid id)
    {
        await _manager.DeleteAsync<BlockDal>(id);
        return Ok();
    }
    
    /// <summary>
    /// рест на получение всех блоков задач
    /// </summary>
    [HttpGet("all")]
    [ProducesResponseType(typeof(BlockResponse), 200)]
    public IActionResult GetAllBlockAsync()
    {
        var blocks = _manager.GetAll<BlockDal>();
        if (blocks == null)
            throw new BlockNotFoundException();
        return Ok(new AllBlockResponse
        {
            AllBlocks = blocks
        });
    }
}