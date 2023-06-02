using System.IdentityModel.Tokens.Jwt;
using Dal.Entities;
using IdentityServer4.Extensions;
using Logic.Exceptions.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HRelloApi.Attributes;

public class CustomAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly UserManager<UserDal> _userManager;

    /*internal CustomAuthorizeAttribute(UserManager<UserDal> userManager)
    {
        _userManager = userManager;
    }*/

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Request.Headers["Authorization"];
        if (token.IsNullOrEmpty())
        {
            // not logged in
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            return;
        }

        if (Roles != null)
        {
            var clearToken = token.ToString().Split(" ")[1];
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadToken(clearToken) as JwtSecurityToken;
            try
            {
                var role = jwt.Claims.First(x => x.Type == "Role").Value;
                if (role != Roles)
                {
                    context.Result = new JsonResult(new { message = "Unauthorized" })
                        { StatusCode = StatusCodes.Status403Forbidden };
                }
            }
            catch (Exception e)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" })
                    { StatusCode = StatusCodes.Status403Forbidden };
            }
        }
    }

    public string? Roles { get; set; } 
}