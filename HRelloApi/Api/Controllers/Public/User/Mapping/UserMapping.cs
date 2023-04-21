using AutoMapper;
using Dal.Entities;
using HRelloApi.Controllers.Public.User.dto.Request;
using HRelloApi.Controllers.Public.User.dto.Response;

namespace HRelloApi.Controllers.Public.User.Mapping;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<UpdateUserRequest, UserDal>()
            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dst => dst.Surname, opt => opt.MapFrom(src => src.Surname))
            .ForMember(dst => dst.Patronymic, opt => opt.MapFrom(src => src.Patronymic))
            .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dst => dst.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
            ;
        
        CreateMap<UserDal, GetUserResponse>()
            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dst => dst.Surname, opt => opt.MapFrom(src => src.Surname))
            .ForMember(dst => dst.Patronymic, opt => opt.MapFrom(src => src.Patronymic))
            .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dst => dst.EmailConfirmed, opt => opt.MapFrom(src => src.EmailConfirmed))
            .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dst => dst.DepartamentId, opt => opt.MapFrom(src => src.DepartamentId))
            ;
    }
}