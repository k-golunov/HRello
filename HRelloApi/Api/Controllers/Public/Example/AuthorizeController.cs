using Dal.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HRelloApi.Controllers.Public.Example;

[ApiController]
[Route("api/v1/public/[controller]")]
public class AuthorizeController
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JWTSettings _options;

    public AuthorizeController(SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager, IOptions<JWTSettings> options)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _options = options.Value;
    }

    //todo!
    [HttpPost("register")]
    public async Task<IActionResult> Register()
    {
        var user = new UserDal();
        await _signInManager.SignInAsync(user, isPersistent: false);
        return new OkResult();
    }
}