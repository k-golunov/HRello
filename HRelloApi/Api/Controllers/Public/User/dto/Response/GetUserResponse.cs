using Dal.Entities;

namespace HRelloApi.Controllers.Public.User.dto.Response;

/// <summary>
/// Модель ответа для получения юзера
/// </summary>
public record GetUserResponse()
{
    public required UserDal User { get; init; }
}