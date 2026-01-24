using AutoMapper;
using BillingService.Domain.Entities;
using Framework.Entities;


namespace SGen.Web.Infrastructure.Mapper.MappingProfiles
{
    public class UnitProfile : Profile
    {
        public UnitProfile() 
        {
            CreateMap<Unit, UnitDto>().ReverseMap();
        }
    }
}
