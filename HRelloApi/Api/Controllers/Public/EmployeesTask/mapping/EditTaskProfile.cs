using AutoMapper;
using Dal.Tasks.Entities;
using HRelloApi.Controllers.Public.Task.dto.request;

namespace HRelloApi.Controllers.Public.Task.mapping;

/// <summary>
/// 
/// </summary>
public class EditTaskProfile: Profile
{
    /// <summary>
    /// 
    /// </summary>
    public EditTaskProfile()
    {
        CreateMap<EditTaskRequest, TaskDal>()
            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dst => dst.Year, opt => opt.MapFrom(src => src.Year))
            .ForMember(dst => dst.Quarter, opt => opt.MapFrom(src => src.Quarter))
            .ForMember(dst => dst.Category, opt => opt.MapFrom(src => src.Category))            .ForMember(dst => dst.Year, opt => opt.MapFrom(src => src.Year))
            .ForMember(dst => dst.Block, opt => opt.MapFrom(src => src.Block))
            .ForMember(dst => dst.PlannedWeight, opt => opt.MapFrom(src => src.PlannedWeight))
            .ForMember(dst => dst.WaitResult, opt => opt.MapFrom(src => src.WaitResult))
            ;



    }
}