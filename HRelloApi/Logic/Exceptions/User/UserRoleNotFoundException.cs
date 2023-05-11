using Logic.Exceptions.Base;

namespace Logic.Exceptions.User;

/// <summary>
/// Ошибка при не найденной роли
/// </summary>
public class UserRoleNotFoundException: BaseNotFoundException
{
    public UserRoleNotFoundException(string role): base("UserRoleNotFoundException", $"Роль {role} не найдена")
    {
        
    }
}