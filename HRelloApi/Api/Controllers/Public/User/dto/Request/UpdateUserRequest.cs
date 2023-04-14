using System.ComponentModel.DataAnnotations;
using Dal.Entities;

namespace HRelloApi.Controllers.Public.User.dto.Request;

public record UpdateUserRequest()
{
    /// <summary>
    /// 
    /// </summary>
    public required string Id { get; init; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    [MaxLength(255)]
    public required string Name { get; init; }
    
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    [MaxLength(255)]
    public required string Surname { get; init; }
    
    /// <summary>
    /// Отчество пользователя
    /// </summary>
    [MaxLength(255)]
    public required string Patronymic { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public required string Email { get; init; }
}