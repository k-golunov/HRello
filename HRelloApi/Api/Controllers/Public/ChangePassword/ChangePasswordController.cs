using Dal.Entities;
using Dal.User.Repositories;
using Dal.User.Repositories.Interfaces;
using HRelloApi.Controllers.Base.Exception;
using HRelloApi.Controllers.Public.Base;
using HRelloApi.Controllers.Public.ChangePassword.dto.request;
using HRelloApi.Notification;
using Logic.Exceptions.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRelloApi.Controllers.Public.ChangePassword;

/// <summary>
/// 
/// </summary>
public class ChangePasswordController : BasePublicController
{
    private readonly UserManager<UserDal> _userManager;
    private readonly IUserRepository _userRepository;
    
    public ChangePasswordController(UserManager<UserDal> userManager, IUserRepository userRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
    }

    /// <summary>
    /// Старт операции смены пароля
    /// отправляет сообщение на почту с ссылкой для смены пароля
    /// </summary>
    /// <returns></returns>
    [HttpPost("start")]
    public async Task<IActionResult> StartChangePassword([FromBody] StartChangePasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return NotFound(new BaseExceptionModel("User.404", "User not found"));
        
        EmailSender.SendEmail($"Перейдите по ссылке для смены пароля : http://185.133.40.145:3000/change-password/{user.Id}", request.Email);
        
        return Ok();
    }
    
    /// <summary>
    /// Cмена пароля
    /// </summary>
    /// <returns></returns>
    [HttpPost("{userId}")]
    public async Task<IActionResult> ChangePassword([FromRoute] string userId, [FromBody] ChangePasswordRequest request)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return NotFound(new BaseExceptionModel("User.404", "User not found"));
        //var response = await _userManager.ResetPasswordAsync(user, "", request.NewPassword);
       var password =  _userManager.PasswordHasher.HashPassword(user, request.NewPassword);
       user.PasswordHash = password;
       await _userRepository.ChangePasswordAsync(user);
        
        return Ok();
    }
}