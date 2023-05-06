/*using System.Security.Principal;
using Dal.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Claims;
using Claim = System.Security.Claims.Claim;
using ClaimsIdentity = System.Security.Claims.ClaimsIdentity;
using ClaimsPrincipal = System.Security.Claims.ClaimsPrincipal;

namespace Logic.Managers.Identity.Helpers;

public class ClaimsTransformationModule : ClaimsAuthenticationManager
{
    private readonly UserManager<UserDal> _userManager;

    public ClaimsTransformationModule(UserManager<UserDal> userManager)
    {
        _userManager = userManager;
    }
    
    public override IClaimsPrincipal Authenticate(string resourceName, IClaimsPrincipal claimsPrincipal)
    {
        if (claimsPrincipal != null && claimsPrincipal.Identity.IsAuthenticated)
        {
            var identity = (ClaimsIdentity)claimsPrincipal.Identity;
            var user = await _userManager.FindByNameAsync(identity.Name);

            identity.AddClaim(new Claim("fullname", user.UserName)));
            identity.AddClaim(new Claim("avatarUrl", user.AvatarUrl));
        }

        return claimsPrincipal;
    }
} */