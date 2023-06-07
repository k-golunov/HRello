using Dal.Entities;
using HRelloApi.Hubs.Auth.dto.request;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace HRelloApi.Hubs.Auth;

/// <summary>
/// Хаб для уведомлений по авторизации
/// </summary>
public class AuthHub : Hub
{
    private readonly UserManager<UserDal> _userManager;

    public AuthHub(UserManager<UserDal> userManager)
    {
        _userManager = userManager;
    }


    /// <summary>
    /// Послать всем подписчикам уведомление, что пользователь зарегистрирован
    /// </summary>
    /// <param name="request"></param>
    public async Task SendNotificationUpdateTask(NewUserNotificationRequest request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        
        await Clients.All.SendAsync("UpdateTaskReceive", $"{user.Name} {user.Surname} {user.Patronymic} успешно прошел регистрацию!");
    }
}