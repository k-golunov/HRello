using System.ComponentModel.DataAnnotations;
using Dal.Entities;

namespace HRelloApi.Controllers.Public.User.dto.Response;

/// <summary>
/// Модель ответа для получения всех юзеров
/// </summary>
public record GetAllUserResponse
{
    public required IList<UserDal> Users { get; init; }
}