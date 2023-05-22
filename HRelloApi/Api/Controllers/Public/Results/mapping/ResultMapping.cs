using AutoMapper;
using Dal.TaskResult.Entities;
using HRelloApi.Controllers.Public.Results.dto.Request;
using HRelloApi.Controllers.Public.Results.dto.Response;

namespace HRelloApi.Controllers.Public.Results.mapping;

public class ResultMapping : Profile
{
    public ResultMapping()
    {
        CreateMap<TaskResultDal, GetTaskResultResponse>()
            .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dst => dst.Color, opt => opt.MapFrom(src => src.Color))
            .ForMember(dst => dst.Result, opt => opt.MapFrom(src => src.Result))
            .ForMember(dst => dst.Tasks, opt => opt.MapFrom(src => src.Tasks))
            .ForMember(dst => dst.Year, opt => opt.MapFrom(src => src.Tasks.First().Year))
            .ForMember(dst => dst.Quarter, opt => opt.MapFrom(src => src.Tasks.First().Quarter))
            ;
        
        CreateMap<CreateTaskResultRequest, TaskResultDal>()
            .ForMember(dst => dst.Color, opt => opt.MapFrom(src => src.Color))
            .ForMember(dst => dst.Result, opt => opt.MapFrom(src => src.Result))
            .ForMember(dst => dst.Tasks, opt => opt.Ignore())
            .ForMember(dst => dst.Id, opt => opt.Ignore())
            ;
    }    
}