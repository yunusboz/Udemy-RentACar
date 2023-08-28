using Application.Features.Models.Queries.GetList;
using Application.Features.Models.Queries.GetListByDynamic;
using AutoMapper;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Paginate<Model>, GetListResponse<GetListModelListItemDto>>().ReverseMap();
        CreateMap<Paginate<Model>, GetListResponse<GetListByDynamicModelListItemDto>>().ReverseMap();

        CreateMap<Model, GetListModelListItemDto>()
            .ForMember(destinationMember: m => m.BrandName, memberOptions: opt => opt.MapFrom(m => m.Brand.Name))
            .ForMember(destinationMember: m => m.FuelName, memberOptions: opt => opt.MapFrom(m => m.Fuel.Name))
            .ForMember(destinationMember: m => m.TransmissionName, memberOptions: opt => opt.MapFrom(m => m.Transmission.Name))
            .ReverseMap();

        CreateMap<Model, GetListByDynamicModelListItemDto>()
            .ForMember(destinationMember: m => m.BrandName, memberOptions: opt => opt.MapFrom(m => m.Brand.Name))
            .ForMember(destinationMember: m => m.FuelName, memberOptions: opt => opt.MapFrom(m => m.Fuel.Name))
            .ForMember(destinationMember: m => m.TransmissionName, memberOptions: opt => opt.MapFrom(m => m.Transmission.Name))
            .ReverseMap();
    }
}
