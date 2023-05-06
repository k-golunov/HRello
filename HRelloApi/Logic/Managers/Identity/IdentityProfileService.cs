using Dal.Entities;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace Logic.Managers.Identity;

public class IdentityProfileService : IProfileService
{
    private readonly IUserClaimsPrincipalFactory<UserDal> _claimsFactory;
    private readonly UserManager<UserDal> _userManager;

    public IdentityProfileService(IUserClaimsPrincipalFactory<UserDal> claimsFactory, UserManager<UserDal> userManager)
    {
        _claimsFactory = claimsFactory;
        _userManager = userManager;
    }
    
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var sub = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(sub);
        if (user == null)
        {
            throw new ArgumentException("");
        }

        var principal = await _claimsFactory.CreateAsync(user);
        var claims = principal.Claims.ToList();

        //Add more claims like this
        //claims.Add(new System.Security.Claims.Claim("departamentId", user.DepartamentId.ToString()));

        context.IssuedClaims = claims;
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var sub = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(sub);
        context.IsActive = user != null;
    }
}