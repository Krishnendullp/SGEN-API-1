using AutoMapper;
using BillingService.Domain.Entities;
using Framework.Entities;
using System;

namespace SGen.Framework.Web.Mapper.MappingProfiles
{
    public class TaxProfiles : Profile
    {
        public TaxProfiles() 
        { 
            CreateMap<Tax , TaxDto>().ReverseMap();
            CreateMap<TaxGroup, TaxGroupDto>().ReverseMap();
            CreateMap<TaxGroupSgstDetail, TaxGroupSgstDetailDto>().ReverseMap();
            CreateMap<TaxGroupIgstDetail, TaxGroupIgstDetailDto>().ReverseMap();
        }

    }
}
