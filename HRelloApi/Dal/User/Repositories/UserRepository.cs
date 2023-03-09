using Dal.Entities;
using Dal.User.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Dal.User.Repositories;

/// <summary>
/// Репозиторий, который должен быть реализацией над айдентити
/// В данные момент не реализовано.
/// </summary>
public class UserRepository : SignInManager<UserDal>
{
    public UserRepository(UserManager<UserDal> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<UserDal> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<UserDal>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<UserDal> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
    {
    }
}