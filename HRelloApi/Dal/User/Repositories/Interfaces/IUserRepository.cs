using Dal.Entities;

namespace Dal.User.Repositories.Interfaces;

public interface IUserRepository
{
    public Task ChangePasswordAsync(UserDal dal);
}