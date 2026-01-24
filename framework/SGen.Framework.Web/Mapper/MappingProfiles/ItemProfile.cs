using AutoMapper;
using BillingService.Domain.Entities;
using Framework.Entities;


namespace SGen.Framework.Web.Mapper.MappingProfiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile() 
        {
            CreateMap<ItemMaster, ItemMasterDto>().ReverseMap();
            CreateMap<Unit, UnitDto>().ReverseMap();
            CreateMap<TaxGroup, TaxGroupDto>().ReverseMap();
            CreateMap<TaxGroupIgstDetail, TaxGroupIgstDetailDto>().ReverseMap();
            CreateMap<TaxGroupSgstDetail, TaxGroupSgstDetailDto>().ReverseMap();
            CreateMap<Tax, TaxDto>().ReverseMap();  
        }
    }
}
