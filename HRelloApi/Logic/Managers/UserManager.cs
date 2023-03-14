using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Logic.Managers;

public class UserManager<TUser>: Microsoft.AspNetCore.Identity.UserManager<TUser> where TUser : class
{
    public async Task<IdentityResult> UpdatePasswordAsync(TUser user, string newPassword)
    {
        var result = await UpdatePasswordHash(user, newPassword, false);
        return result;
    }

    public UserManager(IUserStore<TUser> store, 
        IOptions<IdentityOptions> optionsAccessor, 
        IPasswordHasher<TUser> passwordHasher, 
        IEnumerable<IUserValidator<TUser>> userValidators, 
        IEnumerable<IPasswordValidator<TUser>> passwordValidators, 
        ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, 
        IServiceProvider services, 
        ILogger<Microsoft.AspNetCore.Identity.UserManager<TUser>> logger)
        : base(store, 
            optionsAccessor, 
            passwordHasher, 
            userValidators, 
            passwordValidators, 
            keyNormalizer, 
            errors, 
            services, 
            logger)
    {
    }
}