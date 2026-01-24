using AutoMapper;
using BillingService.Domain.Entities;
using SGen.Framework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGen.Framework.Web.Mapper.MappingProfiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerMaster, CustomerMasterDto>().ReverseMap();
        }
            
    }
}
