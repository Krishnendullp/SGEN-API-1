using AutoMapper;
using BillingService.Domain.Entities;
using Framework.Entities;


namespace SGen.Framework.Web.Mapper.MappingProfiles
{
    public class PurchaseProfile : Profile
    {
        public PurchaseProfile() 
        {
            CreateMap<PurchaseOrder, PurchaseOrderDto>().ReverseMap();
            CreateMap<ProjectMaster, ProjectMasterDto>().ReverseMap();
            CreateMap<StoreTransaction, StoreTransactionDto>().ReverseMap();
            CreateMap<ItemMaster, ItemMasterDto>().ReverseMap();
            CreateMap<StoreTransactionTax, StoreTransactionTaxDto>().ReverseMap();
        }
    }
}
