using Dal.Entities;

namespace Dal.User.Repositories.Interfaces;

public interface IUserRepository
{
    /// <summary>
    /// Сменить пароль 
    /// </summary>
    /// <param name="dal"></param>
    /// <returns></returns>
    public Task ChangePasswordAsync(UserDal dal);

    /// <summary>
    /// Получить идентификатор отдела отдел пользователя
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<int> GetDepartmentId(string userId);
}