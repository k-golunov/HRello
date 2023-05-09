﻿using AutoMapper;
using Dal.Entities;
using HRelloApi.Controllers.Public.Departament.Dto.Request;

namespace HRelloApi.Controllers.Public.Departament.Mapping;

public class DepartamentProfiles : Profile
{
    public DepartamentProfiles()
    {
        CreateMap<CreateDepartamentRequest, DepartamentDal>()
            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dst => dst.BossId, opt => opt.Ignore())
            .ForMember(dst => dst.Id, opt => opt.Ignore())
            ;
    }
}