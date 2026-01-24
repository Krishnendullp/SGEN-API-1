using AutoMapper;
using BillingService.Application.Interfaces;
using BillingService.Domain.Entities;
using Framework.Entities;
using Framework.Repositories;
using Framework.Services.Implementations;


namespace BillingService.Application.Services;

public class ProjectService : CrudService<ProjectMaster, ProjectMasterDto, long>, IProjectService
{
    public ProjectService(IQueryRepository<ProjectMaster, long> repository, IMapper mapper, ICommandRepository<ProjectMaster, long> commandRepo)
        : base(repository, mapper, commandRepo)
    {
    }
}
