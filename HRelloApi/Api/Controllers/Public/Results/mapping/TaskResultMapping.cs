using AutoMapper;
using Dal.TaskResult.Entities;
using HRelloApi.Controllers.Public.Results.dto.Request;
using HRelloApi.Controllers.Public.Results.dto.Response;

namespace HRelloApi.Controllers.Public.Results.mapping;

public class TaskResultMapping : Profile
{
    public TaskResultMapping()
    {
        CreateMap<TaskResultDal, GetTaskResultResponse>()
            .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dst => dst.Color, opt => opt.MapFrom(src => src.Color))
            .ForMember(dst => dst.Result, opt => opt.MapFrom(src => src.Result))
            .ForMember(dst => dst.Tasks, opt => opt.MapFrom(src => src.Tasks))
            //.ForMember(dst => dst.Tasks.First().Year, opt => opt.MapFrom(src => src.Year))
            //.ForMember(dst => dst.Tasks.First().Quarter, opt => opt.MapFrom(src => src.Quarter))
            ;
        
        CreateMap<CreateTaskResultRequest, TaskResultDal>()
            .ForMember(dst => dst.Color, opt => opt.MapFrom(src => src.Color))
            .ForMember(dst => dst.Result, opt => opt.MapFrom(src => src.Result))
            .ForMember(dst => dst.Tasks.Select(a => a.Id), opt => opt.MapFrom(src => src.TasksId))
            .ForMember(dst => dst.Id, opt => opt.Ignore())
            ;
    }    
}