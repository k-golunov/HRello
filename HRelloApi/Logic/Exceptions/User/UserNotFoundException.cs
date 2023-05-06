using Logic.Exceptions.Base;

namespace Logic.Exceptions.User;

public class UserNotFoundException: BaseException
{
    public UserNotFoundException() : base("UserNotFoundException", "Пользователь не найден", 404)
    {
        
    }
}