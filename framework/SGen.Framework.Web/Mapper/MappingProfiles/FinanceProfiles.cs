using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BillingService.Domain.Entities;
using SGen.Framework.Entities;


namespace SGen.Framework.Web.Mapper.MappingProfiles
{
    public class FinanceProfiles : Profile
    {
        public FinanceProfiles() 
        {
            CreateMap<LedgerCategory, LedgerCategoryDto>().ReverseMap();
            CreateMap<LedgerMaster, LedgerMasterDto>().ReverseMap();
            CreateMap<SubLedger, SubLedgerDto>().ReverseMap();
            CreateMap<VoucherMaster, VoucherMasterDto>().ReverseMap();
            CreateMap<LedgerDetail, LedgerDetailDto>().ReverseMap();
        }
        
    }
}
