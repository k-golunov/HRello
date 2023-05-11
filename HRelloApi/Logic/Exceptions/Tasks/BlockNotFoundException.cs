using Logic.Exceptions.Base;

namespace Logic.Exceptions.Tasks;

/// <summary>
/// Ошибка при не найденном блоке
/// </summary>
public class BlockNotFoundException: BaseNotFoundException
{
    public BlockNotFoundException(Guid id): base("BlockNotFoundException", $"Блок с id - {id} не найден")
    {
        
    }
}