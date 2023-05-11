using Logic.Exceptions.Base;

namespace Logic.Exceptions.User;

/// <summary>
/// Ошибка при не найденном пользователе
/// </summary>
public class UserNotFoundException: BaseNotFoundException
{
    public UserNotFoundException(string id) : base("UserNotFoundException", $"Пользователь с id - {id} не найден")
    {
        
    }
}