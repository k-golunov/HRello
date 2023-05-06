using Microsoft.AspNetCore.Authentication;

namespace Logic.Managers.Identity.Helpers;

/// <summary>
/// Класс для получения разной инфы по авторизации через айдентити и токен
/// </summary>
public class IdentityHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public IdentityHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    /// <summary>
    /// Получение айди юзера из токена
    /// </summary>
    /// <returns></returns>
    public string? GetUserId()
    {
        //var token = await context.GetTokenAsync("Bearer", "access_token");
        //var userId = _httpContextAccessor.HttpContext.Request.Headers.A
        var userId = _httpContextAccessor.HttpContext.Request.HttpContext.User.Identity.Name;
        return userId;
    }
}