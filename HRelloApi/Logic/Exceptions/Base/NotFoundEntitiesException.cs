namespace Logic.Exceptions.Base;

public class NotFoundEntitiesException : BaseException
{
    public NotFoundEntitiesException() : base("EntitiesNotFound.404", "Ни одной сущности не найдено", 404)
    {
        
    }
    
    public NotFoundEntitiesException(string typeEntity) : base("EntitiesNotFound.404", $"Ни одной сущности {typeEntity} не найдено", 404)
    {
        
    }
}