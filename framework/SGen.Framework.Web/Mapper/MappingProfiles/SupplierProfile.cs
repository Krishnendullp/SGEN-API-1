using AutoMapper;
using BillingService.Domain.Entities;
using Framework.Entities;

namespace SGen.Web.Infrastructure.Mapper.MappingProfiles
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile() 
        {
            CreateMap<SupplierMaster, SupplierMasterDto>().ReverseMap();
            CreateMap<SupplierPayment, SupplierPaymentDto>().ReverseMap();
            CreateMap<SupplierReturn, SupplierReturnDto>().ReverseMap();
            CreateMap<SupplierReturnDetail, SupplierReturnDetailDto>().ReverseMap();
        }
    }
}
