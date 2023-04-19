using Dal.Entities;

namespace HRelloApi.Controllers.Public.User.dto.Response;

/// <summary>
/// Модель ответа для получения юзера
/// </summary>
public record GetUserResponse()
{
    /// <summary>
    /// 
    /// </summary>
    public required string Id { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public required string Name { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public required string Surname { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public required string Patronymic { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public required int DepartamentId { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public required string Email { get; init; }
    
    /// <summary>
    /// 
    /// </summary>
    public required bool EmailConfirmed { get; init; }
    
}