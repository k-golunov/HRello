﻿using AutoMapper;
using Dal.Tasks.Entities;
using HRelloApi.Controllers.Public.Tasks.dto.request;
using HRelloApi.Controllers.Public.Tasks.dto.response;

namespace HRelloApi.Controllers.Public.Tasks.mapping;

/// <summary>
/// класс отвечающий за маппинг сущности итогов руководителя
/// </summary>
public class BossResultProfile: Profile
{
    /// <summary>
    /// Конструтор и маппинг
    /// </summary>
    public BossResultProfile()
    {
        CreateMap<BossTaskCompletedRequest, BossTaskResultDal>()
            .ForMember(dst => dst.Result, opt => opt.MapFrom(src => src.Result))
            .ForMember(dst => dst.Comment, opt => opt.MapFrom(src => src.Comment))
            .ForMember(dst => dst.TaskId, opt => opt.MapFrom(src => src.TaskId));

        CreateMap<BossTaskResultDal, BossResultResponse>()
            .ForMember(dst => dst.Result, opt => opt.MapFrom(src => src.Result))
            .ForMember(dst => dst.Comment, opt => opt.MapFrom(src => src.Comment))
            .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id));

    }
}