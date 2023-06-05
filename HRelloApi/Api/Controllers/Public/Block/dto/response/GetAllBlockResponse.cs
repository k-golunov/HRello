using Dal.Tasks.Entities;

namespace HRelloApi.Controllers.Public.Block.dto.response;

/// <summary>
/// Модель для получения всех блоков
/// </summary>
public record GetAllBlockResponse
{
    /// <summary>
    /// Все блоки
    /// </summary>
    public required List<BlockDal> AllBlocks { get; init; }
}