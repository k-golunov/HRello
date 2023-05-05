using Dal.Base.Entitities;

namespace Dal.Tasks.Entities;

public class BlockDal : BaseDal<Guid>
{
    public string Value { get; set; }

    public BlockDal()
    {
        
    }
    public BlockDal(string value)
    {
        Value = value;
    }
}