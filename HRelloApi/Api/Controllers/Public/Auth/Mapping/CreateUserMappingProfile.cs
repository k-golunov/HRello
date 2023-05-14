using AutoMapper;
using Dal.Entities;
using HRelloApi.Controllers.Public.Auth.Dto.Request;
using Microsoft.AspNetCore.Identity;

namespace HRelloApi.Controllers.Public.Auth.Mapping;

/// <summary>
/// Класс маппинга модели данных создаваемого пользователя в базовую сущность
/// </summary>
public class CreateUserMappingProfile: Profile
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public CreateUserMappingProfile()
    {
        CreateMap<CreateUserModelRequest, UserDal>()
            .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email));
    }
}