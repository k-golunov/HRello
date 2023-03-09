using Dal.Entities;
using Dal.User.Repositories;
using Dal.User.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRelloApi.Controllers.Public.Example;

public class ExampleController : ControllerBase
{

    [HttpPost("test")]
    public async void Get([FromServices] UserRepository userRepository)
    {
        await userRepository.CreateUserPrincipalAsync(new UserDal
        {
            Email = "avc@mail.ru",
        });
        await userRepository.UserManager.CreateAsync(new UserDal()
        {
            Email = "avc@mail.ru",
        });
    }
}