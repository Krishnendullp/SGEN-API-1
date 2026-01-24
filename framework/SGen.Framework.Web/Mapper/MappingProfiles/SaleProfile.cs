using AutoMapper;
using BillingService.Domain.Entities;
using Framework.Entities;
using SGen.Framework.Entities;


namespace SGen.Framework.Web.Mapper.MappingProfiles
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<SaleInvoicePayment, SaleInvoicePaymentDto>().ReverseMap();
            CreateMap<ProjectMaster, ProjectMasterDto>().ReverseMap();
            CreateMap<SaleMaster, SaleMasterDto>().ReverseMap();
            CreateMap<SaleMasterDetail, SaleMasterDetailDto>().ReverseMap();
            CreateMap<SaleItemMaster, SaleItemMasterDto>().ReverseMap();
            CreateMap<SaleDetailTax, SaleDetailTaxDto>().ReverseMap();
            CreateMap<SaleMasterTax, SaleMasterTaxDto>().ReverseMap();
            CreateMap<TaxGroup, TaxGroupDto>().ReverseMap();
        }
    }
}
