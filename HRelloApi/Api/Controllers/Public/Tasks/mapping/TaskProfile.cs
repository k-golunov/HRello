using AutoMapper;
using Dal.Tasks.Entities;
using HRelloApi.Controllers.Public.Task.dto.request;
using HRelloApi.Controllers.Public.Tasks.dto.request;
using HRelloApi.Controllers.Public.Tasks.dto.response;

namespace HRelloApi.Controllers.Public.Tasks.mapping;

/// <summary>
/// класс отвечающий за маппинг сущности задачи
/// </summary>
public class TaskProfile: Profile
{
    /// <summary>
    /// Конструтор и маппинг
    /// </summary>
    public TaskProfile()
    {
        CreateMap<CreateTaskRequest, TaskDal>()
            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dst => dst.Year, opt => opt.MapFrom(src => src.Year))
            .ForMember(dst => dst.Quarter, opt => opt.MapFrom(src => src.Quarter))
            .ForMember(dst => dst.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dst => dst.PlannedWeight, opt => opt.MapFrom(src => src.PlannedWeight))
            .ForMember(dst => dst.WaitResult, opt => opt.MapFrom(src => src.WaitResult));
        
        CreateMap<EditTaskRequest, TaskDal>()
            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dst => dst.Year, opt => opt.MapFrom(src => src.Year))
            .ForMember(dst => dst.Quarter, opt => opt.MapFrom(src => src.Quarter))
            .ForMember(dst => dst.Category, opt => opt.MapFrom(src => src.Category))            
            .ForMember(dst => dst.Year, opt => opt.MapFrom(src => src.Year))
            .ForMember(dst => dst.PlannedWeight, opt => opt.MapFrom(src => src.PlannedWeight))
            .ForMember(dst => dst.WaitResult, opt => opt.MapFrom(src => src.WaitResult))
            ;
        
        CreateMap<TaskDal, TaskResponse>()
            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dst => dst.Year, opt => opt.MapFrom(src => src.Year))
            .ForMember(dst => dst.Quarter, opt => opt.MapFrom(src => src.Quarter))
            .ForMember(dst => dst.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dst => dst.Block, opt => opt.MapFrom(src => src.Block.Value))
            .ForMember(dst => dst.PlannedWeight, opt => opt.MapFrom(src => src.PlannedWeight))
            .ForMember(dst => dst.WaitResult, opt => opt.MapFrom(src => src.WaitResult))
            .ForMember(dst => dst.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dst => dst.UserId, opt => opt.MapFrom(src => src.User.Id))
            .ForMember(dst => dst.DepartmentId, opt => opt.MapFrom(src => src.DepartamentId));
    }
}