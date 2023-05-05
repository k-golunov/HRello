using Logic.Exceptions.Base;

namespace Logic.Exceptions.Tasks;

public class BlockNotFoundException: BaseException
{
    public BlockNotFoundException(): base("BlockNotFoundException", "Блок не найден", 404)
    {
        
    }
}