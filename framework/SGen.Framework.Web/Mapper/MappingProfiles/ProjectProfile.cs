using AutoMapper;
using BillingService.Domain.Entities;
using Framework.Entities;


namespace SGen.Framework.Web.Mapper.MappingProfiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectMaster, ProjectMasterDto>().ReverseMap();
        }
    }
}
