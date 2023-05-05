using Dal.Base;
using Dal.Tasks.Entities;
using Dal.Tasks.Repositories.Interfaces;

namespace Dal.Tasks.Repositories;

public class BlockRepository : BaseRepository<BlockDal, Guid>, IBlockRepository
{
    public BlockRepository(DataContext context) : base(context)
    {
    }
}