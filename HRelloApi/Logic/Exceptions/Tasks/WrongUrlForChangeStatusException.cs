using Logic.Exceptions.Base;

namespace Logic.Exceptions.Tasks;

/// <summary>
/// Ошибка при запросе на не правильный рест при смене статусов
/// </summary>
public class WrongUrlForChangeStatusException: BaseException
{
    public WrongUrlForChangeStatusException(string url): base("WrongUrlForChangeStatusException",$"Неверный адрес запроса для данной смены статуса. \n Используйте {url}", 400)
    {
        
    }
}