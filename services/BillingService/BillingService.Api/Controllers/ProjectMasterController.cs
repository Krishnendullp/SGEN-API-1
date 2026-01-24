using BillingService.Application.Interfaces;
using BillingService.Domain.Entities;
using Framework.Entities;
using Framework.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BillingService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectMasterController : BaseCrudController<ProjectMaster, ProjectMasterDto, long>
{
    public ProjectMasterController(IProjectService service) : base(service)
    {
    }
}