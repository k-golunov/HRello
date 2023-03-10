using AutoMapper;
using Dal.Entities;
using HRelloApi.Controllers.Public.Example.Dto.Request;

namespace HRelloApi.Controllers.Public.Example.Mapping;

public class AccountMappingProfile: Profile
{
    public AccountMappingProfile()
    {
        CreateMap<RegisterModelRequest, UserDal>()
            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dst => dst.Surname, opt => opt.MapFrom(src => src.Surname))
            .ForMember(dst => dst.Patronymic, opt => opt.MapFrom(src => src.Patronymic));
    }
}