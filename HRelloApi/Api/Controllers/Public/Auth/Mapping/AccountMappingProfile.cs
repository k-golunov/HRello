using AutoMapper;
using Dal.Entities;
using HRelloApi.Controllers.Public.Auth.Dto.Request;

namespace HRelloApi.Controllers.Public.Auth.Mapping;

/// <summary>
/// Класс маппинга модели данных регистрируемого пользователя в базовую сущность
/// </summary>
public class AccountMappingProfile: Profile
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public AccountMappingProfile()
    {
        CreateMap<RegisterModelRequest, UserDal>()
            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dst => dst.Surname, opt => opt.MapFrom(src => src.Surname))
            .ForMember(dst => dst.Patronymic, opt => opt.MapFrom(src => src.Patronymic));
    }
}