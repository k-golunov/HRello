using AutoMapper;
using Dal.Tasks.Entities;
using HRelloApi.Controllers.Public.Tasks.dto.request;
using HRelloApi.Controllers.Public.Tasks.dto.response;

namespace HRelloApi.Controllers.Public.Tasks.mapping;

/// <summary>
/// Класс отвечающий зам маппинг сущности итогов сотрудника
/// </summary>
public class UserResultProfile: Profile
{
    /// <summary>
    /// конструтор-маппер
    /// </summary>
    public UserResultProfile()
    {
        CreateMap<UserTaskCompletedRequest, UserTaskResultDal>()
            .ForMember(dst => dst.FactResult, opt => opt.MapFrom(src => src.FactResult))
            .ForMember(dst => dst.FactWeight, opt => opt.MapFrom(src => src.FactWeight))
            .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dst => dst.TaskId, opt => opt.MapFrom(src => src.TaskId))
            .ForMember(dst => dst.Result, opt => opt.MapFrom(src => src.Result));
        
        CreateMap<UserTaskResultDal, UserResultResponse>()
            .ForMember(dst => dst.FactResult, opt => opt.MapFrom(src => src.FactResult))
            .ForMember(dst => dst.FactWeight, opt => opt.MapFrom(src => src.FactWeight))
            .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dst => dst.Result, opt => opt.MapFrom(src => src.Result));

        CreateMap<EditUserResultRequest, UserTaskResultDal>()
            .ForMember(dst => dst.FactResult, opt => opt.MapFrom(src => src.FactResult))
            .ForMember(dst => dst.FactWeight, opt => opt.MapFrom(src => src.FactWeight))
            .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dst => dst.Result, opt => opt.MapFrom(src => src.Result))
            .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id));

    }
}