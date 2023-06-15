using Dal.Entities;

namespace HRelloApi.Controllers.Public.User.dto.Response;

/// <summary>
/// Модель ответа для получения юзера
/// </summary>
public record GetUserResponse
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public required string Id { get; init; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; init; }
    
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string Surname { get; init; }
    
    /// <summary>
    /// Отчество пользователя
    /// </summary>
    public string Patronymic { get; init; }
    
    /// <summary>
    /// Идентификатор отдела пользователя
    /// </summary>
    public int DepartamentId { get; init; }
    
    /// <summary>
    /// Почта пользователя
    /// </summary>
    public required string Email { get; init; }
    
    /// <summary>
    /// Подтвержденна ли почта пользователя
    /// </summary>
    public bool EmailConfirmed { get; init; }
    
    /// <summary>
    /// Удален ли пользователь (заблокирован)
    /// </summary>
    public bool LockoutEnabled { get; init; }
    
}