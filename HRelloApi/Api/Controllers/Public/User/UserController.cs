using AutoMapper;
using Dal.Entities;
using HRelloApi.Controllers.Base.Exception;
using HRelloApi.Controllers.Public.Base;
using HRelloApi.Controllers.Public.User.dto.Request;
using HRelloApi.Controllers.Public.User.dto.Response;
using Logic.Constants;
using Logic.Exceptions.User;
using Logic.Managers.Departament.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Serilog.Context;

namespace HRelloApi.Controllers.Public.User;

/// <summary>
/// 
/// </summary>
public class UserController : BasePublicController
{
    private readonly SignInManager<UserDal> _signInManager;
    private readonly UserManager<UserDal> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IDepartamentManager _departamentManager;
    private readonly JWTSettings _options;
    private readonly IMapper _mapper;

    /// <summary>
    /// Конструктор контроллера
    /// </summary>
    /// <param name="userManager">Сервис логики работы с пользователями</param>
    /// <param name="signInManager">Сервис логики работы с аутентификацией пользователей</param>
    /// <param name="options">Настройки уникального токена</param>
    /// <param name="mapper">Автомаппер</param>
    public UserController(UserManager<UserDal> userManager, 
        SignInManager<UserDal> signInManager, 
        IOptions<JWTSettings> options,
        IMapper mapper,
        RoleManager<IdentityRole> roleManager)
    {
        LogContext.PushProperty("Source", "Test Authorize Controller");
        _userManager = userManager;
        _signInManager = signInManager;
        _options = options.Value;
        _mapper = mapper;
        _roleManager = roleManager;
    }

    /// <summary>
    /// Получение юзера по id
    /// не тянет связанные данные
    /// </summary>
    /// <param name="id">идентификатор юзера</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(GetUserResponse), 200)]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
            throw new UserNotFoundException(id.ToString());
        var result = _mapper.Map<GetUserResponse>(user);
        return Ok(result);
    }

    /// <summary>
    /// Обновление данных юзера
    /// </summary>
    /// <param name="userUpdate"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(200)]
    public async Task<IActionResult> UpdateUser(UpdateUserRequest userUpdate)
    {
        var user = await _userManager.FindByIdAsync(userUpdate.Id);
        if (user is null)
            throw new UserNotFoundException(userUpdate.Id);
        var userDal = _mapper.Map(userUpdate, user);
        await _userManager.UpdateAsync(userDal);
        return Ok();
    }

    /// <summary>
    /// Получение всех записей из бд юзера
    /// не тянет связанные данные
    /// </summary>
    /// <returns></returns>
    [HttpGet("all")]
    [ProducesResponseType(typeof(GetAllUserResponse), 200)]
    public async Task<IActionResult> GetAllUser()
    {
        var allUsersEmployee = await _userManager.GetUsersInRoleAsync(RoleConstants.Employee);
        var allUsers = allUsersEmployee.Concat(await _userManager.GetUsersInRoleAsync(RoleConstants.Boss))
            .Concat(await _userManager.GetUsersInRoleAsync(RoleConstants.MainBoss)).ToList();
        return Ok(new GetAllUserResponse
        {
            Users = allUsers.Select(_mapper.Map<GetUserResponse>).ToList()
        });
    }
    
    /// <summary>
    /// Полчение всех зарегистрированных пользователей
    /// зарегистрированный - значит подтверждена почта
    /// </summary>
    /// <returns></returns>
    [HttpGet("all-register")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetAllRegisterUser()
    {
        var allUsersEmployee = await _userManager.GetUsersInRoleAsync(RoleConstants.Employee);
        var allUsers = allUsersEmployee.Concat(await _userManager.GetUsersInRoleAsync(RoleConstants.Boss))
            .Concat(await _userManager.GetUsersInRoleAsync(RoleConstants.MainBoss)).ToList();
        var users = allUsers.Where(u => u.EmailConfirmed).ToList();
        return Ok(new GetAllUserResponse
        {
            Users = users.Select(_mapper.Map<GetUserResponse>).ToList()
        });
    }
    
    /// <summary>
    /// выдает всех незарегистрированных пользователей
    /// </summary>
    /// <returns></returns>
    [HttpGet("all-unregister")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetAllUnregisterUser()
    {
        var allUsersEmployee = await _userManager.GetUsersInRoleAsync(RoleConstants.Employee);
        var allUsers = allUsersEmployee.Concat(await _userManager.GetUsersInRoleAsync(RoleConstants.Boss))
            .Concat(await _userManager.GetUsersInRoleAsync(RoleConstants.MainBoss)).ToList();
        var users = allUsers.Where(u => !u.EmailConfirmed).ToList();
        return Ok(new GetAllUserResponse
        {
            Users = users.Select(_mapper.Map<GetUserResponse>).ToList()
        });
    }
}