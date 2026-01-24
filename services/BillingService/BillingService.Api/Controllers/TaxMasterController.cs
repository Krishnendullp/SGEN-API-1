using BillingService.Application.Interfaces;
using BillingService.Application.Services;
using BillingService.Domain.Entities;
using Framework.Entities;
using Framework.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BillingService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[SwaggerTag("TaxMaster")]    // ✅ একি GroupName
public class TaxMasterController : BaseCrudController<Tax, TaxDto, long>
{
    public TaxMasterController(ITaxService service) : base(service)
    {
    }
}

[Route("api/[controller]")]
[ApiController]
[SwaggerTag("TaxMaster")]    // ✅ একি GroupName
public class TaxGroupController : BaseCrudController<TaxGroup, TaxGroupDto, long>
{
    public TaxGroupController(ITaxGroupService service) : base(service)
    {
    }
}

[Route("api/[controller]")]
[ApiController]
[SwaggerTag("TaxMaster")]    // ✅ একি GroupName
public class TaxGroupSgstDetailController : BaseCrudController<TaxGroupSgstDetail, TaxGroupSgstDetailDto, long>
{
    public TaxGroupSgstDetailController(ITaxGroupSgstDetailService service) : base(service)
    {
    }
}

[Route("api/[controller]")]
[ApiController]
[SwaggerTag("TaxMaster")]    // ✅ একি GroupName
public class TaxGroupIgstDetailController : BaseCrudController<TaxGroupIgstDetail, TaxGroupIgstDetailDto, long>
{
    public TaxGroupIgstDetailController(ITaxGroupIgstDetailService service) : base(service)
    {
    }
}

