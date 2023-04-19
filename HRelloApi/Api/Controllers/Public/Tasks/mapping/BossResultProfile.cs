using AutoMapper;
using Dal.Tasks.Entities;
using HRelloApi.Controllers.Public.Tasks.dto.request;

namespace HRelloApi.Controllers.Public.Tasks.mapping;

public class BossResultProfile: Profile
{
    public BossResultProfile()
    {
        CreateMap<BossTaskCompletedRequest, BossTaskResultDal>()
            .ForMember(dst => dst.Result, opt => opt.MapFrom(src => src.Result))
            .ForMember(dst => dst.Comment, opt => opt.MapFrom(src => src.Comment))
            .ForMember(dst => dst.TaskId, opt => opt.MapFrom(src => src.TaskId));

    }
}