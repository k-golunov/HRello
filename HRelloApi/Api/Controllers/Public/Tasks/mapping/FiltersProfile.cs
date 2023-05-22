using AutoMapper;
using HRelloApi.Controllers.Public.Tasks.dto.request;
using Logic.Managers.Tasks.Filters;

namespace HRelloApi.Controllers.Public.Tasks.mapping;

/// <summary>
/// Класс отвечающий за маппинг фильтров
/// </summary>
public class FiltersProfile: Profile
{
    /// <summary>
    /// конструктор-маппер
    /// </summary>
    public FiltersProfile()
    {
        CreateMap<FiltersRequest, Filters>()
            .ForMember(dst => dst.Year, opt => opt.MapFrom(src => src.Year))
            .ForMember(dst => dst.Quarter, opt => opt.MapFrom(src => src.Quarter))
            .ForMember(dst => dst.BlockId, opt => opt.MapFrom(src => src.Block))
            .ForMember(dst => dst.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
            .ForMember(dst => dst.UserId, opt => opt.MapFrom(src => src.User));
    }
}