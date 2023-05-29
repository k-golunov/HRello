using AutoMapper;
using Dal.Tasks.Entities;

namespace Logic.Managers.Tasks.Filters.mapping;

public class FilteredTaskProfile : Profile
{
    public FilteredTaskProfile()
    {
        CreateMap<TaskDal, FilteredTask>()            
            .ForMember(dst => dst.Id, ost => ost.MapFrom(src => src.Id))
            .ForMember(dst => dst.Year, ost => ost.MapFrom(src => src.Year))
            .ForMember(dst => dst.Quarter, ost => ost.MapFrom(src => src.Quarter))
            .ForMember(dst => dst.BlockId, ost => ost.MapFrom(src => src.Block.Id))
            .ForMember(dst => dst.UserId, ost => ost.MapFrom(src => src.User.Id))
            .ForMember(dst => dst.Status, ost => ost.MapFrom(src => src.Status))
            .ForMember(dst => dst.DepartmentId, ost => ost.MapFrom(src => src.DepartamentId));
    }
}