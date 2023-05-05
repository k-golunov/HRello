using AutoMapper;
using HRelloApi.Controllers.Public.Tasks.dto.request;
using Logic.Managers.Tasks.Filters;

namespace HRelloApi.Controllers.Public.Tasks.mapping;

public class FiltersProfile: Profile
{
    public FiltersProfile()
    {
        CreateMap<FiltersRequest, Filters>()
            .ForMember(dst => dst.Year, opt => opt.MapFrom(src => src.Year))
            .ForMember(dst => dst.Quarter, opt => opt.MapFrom(src => src.Quarter))
            .ForMember(dst => dst.Block, opt => opt.MapFrom(src => src.Block))
            .ForMember(dst => dst.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
            .ForMember(dst => dst.User, opt => opt.MapFrom(src => src.User));
    }
}