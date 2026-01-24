using AutoMapper;
using BillingService.Application.Interfaces;
using BillingService.Domain.Entities;
using Framework.Entities;
using Framework.Repositories;
using Framework.Repositories.Implementations;
using Framework.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using SGen.Framework.Contexts;
using SGen.Framework.Services;

namespace BillingService.Application.Services;

public class ItemService : CrudService<ItemMaster, ItemMasterDto, long>, IItemService
{
    private readonly SGenDbContext _context;
    private readonly IIdentityServices _identityService;
    private readonly IMapper _mapper;
    public ItemService(IQueryRepository<ItemMaster, long> repository, 
        IMapper mapper, SGenDbContext context, IIdentityServices identityService,
    ICommandRepository<ItemMaster, long> commandRepo )
        : base(repository, mapper, commandRepo) // ✅ inject করে base এ পাঠাচ্ছি
    {
        _context = context;
        _identityService = identityService;
        _mapper = mapper;
        
    }

    public override async Task<ItemMasterDto> GetByIdAsync(int id)
    {
        try
        {
            var identity = _identityService.GetIdentity();

            // For testing/demo
            identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            // ✅ Include related tables dynamically
            var query = _context.Set<ItemMaster>()    // Example 1
                .Include(x => x.Units)
                .Include(x => x.TaxGroups).ThenInclude(tg => tg.TaxGroupIgstDetails).ThenInclude(ig => ig)
                .Include(x => x.TaxGroups).ThenInclude(tg => tg.TaxGroupSgstDetails).ThenInclude(sg => sg.Taxs)// Example 3
                .Where(x =>
                    x.TenantId == identity.TenantId &&
                    x.CompanyId == identity.CompanyId &&
                    x.IsActive == true &&
                    x.ItemId == id);

            var entity = await query.FirstOrDefaultAsync();

            if (entity == null)
                throw new Exception($"ItemMaster not found with Id={id}");

            // ✅ Map to DTO before returning
            var dto = _mapper.Map<ItemMasterDto>(entity);
            return dto;
        }
        catch (Exception ex)
        {
           //Console.WriteLine($"[ItemService.GetByIdAsync] Error: {ex.Message}");
           throw;
        }
    }

    public override async Task<IEnumerable<ItemMasterDto>> GetAllAsync()
    {
        try
        {
            var identity = _identityService.GetIdentity();

            // For testing/demo
            identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            // ✅ Include related tables dynamically
            var query = _context.Set<ItemMaster>()    // Example 1
                .Include(x => x.Units)
                .Include(x => x.TaxGroups).ThenInclude(tg => tg.TaxGroupIgstDetails).ThenInclude(ig => ig.Taxs)
                .Include(x => x.TaxGroups).ThenInclude(tg => tg.TaxGroupSgstDetails).ThenInclude(sg => sg.Taxs)// Example 3
                .Where(x =>
                    x.TenantId == identity.TenantId &&
                    x.CompanyId == identity.CompanyId &&
                    x.IsActive == true).AsSplitQuery();

            //var query = _context.Set<ItemMaster>()
            //.Include(x => x.Units)
            //.Include(x => x.TaxGroups!)
            //    .ThenInclude(tg => tg.TaxGroupIgstDetails!)
            //.ThenInclude(ig => ig.Taxs)
            //    .Include(x => x.TaxGroups!)
            //.ThenInclude(tg => tg.TaxGroupSgstDetails!)
            //.ThenInclude(sg => sg.Taxs)
            //.Where(x => x.TenantId == identity.TenantId &&
            //    x.CompanyId == identity.CompanyId &&
            //    x.IsActive)
            //.AsSplitQuery();

            var entity = await query.FirstOrDefaultAsync();

            //var entity = await query.FirstOrDefaultAsync();

            if (query == null)
                throw new Exception($"ItemMaster not found ");

            // ✅ Map to DTO before returning
            var dto = _mapper.Map<IEnumerable<ItemMasterDto>>(query);
            return dto;
        }
        catch (Exception ex)
        {
            //Console.WriteLine($"[ItemService.GetByIdAsync] Error: {ex.Message}");
            throw;
        }
    }
}
