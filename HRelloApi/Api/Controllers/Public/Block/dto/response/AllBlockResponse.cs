using Dal.Tasks.Entities;

namespace HRelloApi.Controllers.Public.Block.dto.response;

/// <summary>
/// 
/// </summary>
public record AllBlockResponse()
{
    /// <summary>
    /// 
    /// </summary>
    public List<BlockDal> AllBlocks { get; init; }
}