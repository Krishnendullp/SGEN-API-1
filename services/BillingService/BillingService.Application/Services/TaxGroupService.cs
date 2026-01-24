using AutoMapper;
using BillingService.Application.Interfaces;
using BillingService.Domain.Entities;
using Framework.Entities;
using Framework.Repositories;
using Framework.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using SGen.Framework.Common;
using SGen.Framework.Contexts;
using SGen.Framework.Helper;
using SGen.Framework.Services;
using System;
using System.Data;


namespace BillingService.Application.Services;

public class TaxGroupService : CrudService<TaxGroup, TaxGroupDto, long>, ITaxGroupService
{
    private readonly SGenDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityServices _identityService;
    public TaxGroupService (IQueryRepository<TaxGroup, long> repository, 
        IMapper mapper, 
        ICommandRepository<TaxGroup, long> commandRepo,
        SGenDbContext context,
        IIdentityServices identityService)
        : base(repository, mapper, commandRepo)
    {
        _context = context;
        _mapper = mapper;
        _identityService = identityService;
    }

    public override async Task<TaxGroupDto> CreateAsync(TaxGroupDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        using var trx = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);
        try
        {
            var identity = _identityService.GetIdentity();

            // For demo/static identity
            identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            // 1️⃣ Generate new master ID
            var maxId = await _context.TaxGroups
                .Where(x => x.TenantId == identity.TenantId && x.CompanyId == identity.CompanyId)
                .MaxAsync(x => (int?)x.TaxGroupId) ?? 0;

            var newTaxGroupId = maxId + 1;

            // 2️⃣ Manually map Master (TaxGroup)
            var master = new TaxGroup
            {
                TenantId = identity.TenantId,
                CompanyId = identity.CompanyId,
                TaxGroupId = newTaxGroupId,
                Code = dto.Code,
                Description = dto.Description,
                IsActive = true,
                CreatedBy = string.IsNullOrWhiteSpace(dto.CreatedBy) ? "Admin" : dto.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                TaxGroupIgstDetails = new List<TaxGroupIgstDetail>(),
                TaxGroupSgstDetails = new List<TaxGroupSgstDetail>()
            };

            // 3️⃣ Handle IGST Details
            if (dto.TaxGroupIgstDetails != null && dto.TaxGroupIgstDetails.Any())
            {
                var maxIgstId = await _context.TaxGroupIgstDetails
                        .Where(x => x.TenantId == master.TenantId && x.CompanyId == master.CompanyId)
                        .MaxAsync(x => (int?)x.Id) ?? 0;

                int serialNo = 1;
                foreach (var item in dto.TaxGroupIgstDetails)
                {
                    
                    maxIgstId++;

                    var detail = new TaxGroupIgstDetail
                    {
                        TenantId = master.TenantId,
                        CompanyId = master.CompanyId,
                        TaxGroupId = master.TaxGroupId,
                        Id = maxIgstId++,
                        TaxId = item.TaxId,
                        SerialNo = serialNo,
                        Rate = item.Rate ?? 0m,
                        IsActive = true,
                        CreatedBy = string.IsNullOrWhiteSpace(dto.CreatedBy) ? "Admin" : dto.CreatedBy,
                        CreatedOn = DateTime.UtcNow
                    };

                    master.TaxGroupIgstDetails.Add(detail);
                }
            }

            // 4️⃣ Handle SGST Details
            if (dto.TaxGroupSgstDetails != null && dto.TaxGroupSgstDetails.Any())
            {
                var maxSgstId = await _context.TaxGroupSgstDetails
                        .Where(x => x.TenantId == master.TenantId && x.CompanyId == master.CompanyId)
                        .MaxAsync(x => (int?)x.Id) ?? 0;
                int serialNo = 1;

                foreach (var item in dto.TaxGroupSgstDetails)
                {
                    maxSgstId++;

                    var detail = new TaxGroupSgstDetail
                    {
                        TenantId = master.TenantId,
                        CompanyId = master.CompanyId,
                        TaxGroupId = master.TaxGroupId,
                        Id = maxSgstId,
                        TaxId = item.TaxId,
                        SerialNo = serialNo++,
                        Rate = item.Rate ?? 0m,
                        IsActive = true,
                        CreatedBy = string.IsNullOrWhiteSpace(dto.CreatedBy) ? "Admin" : dto.CreatedBy,
                        CreatedOn = DateTime.UtcNow
                    };

                    master.TaxGroupSgstDetails.Add(detail);
                }
            }

            // 5️⃣ Save Master + Details
            await _context.TaxGroups.AddAsync(master);
            await _context.SaveChangesAsync();
            await trx.CommitAsync();

            
            return dto;
        }
        catch (Exception)
        {
            await trx.RollbackAsync();
            throw;
        }
    }
    //{"taxGroupId":4,"code":"SERVICETAX_15","description":"Service Tax Group 15%","taxGroupIgstDetails":[],"taxGroupSgstDetails":[],"createdBy":""}
    public override async Task<TaxGroupDto> UpdateAsync(TaxGroupDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        using var trx = await _context.Database.BeginTransactionAsync(IsolationLevel.Serializable);
        try
        {
            var identity = _identityService.GetIdentity();

            identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            // 1️⃣ Load existing master with child data
            var master = await _context.TaxGroups
                .Include(x => x.TaxGroupIgstDetails)
                .Include(x => x.TaxGroupSgstDetails)
                .FirstOrDefaultAsync(x =>
                    x.TenantId == identity.TenantId &&
                    x.CompanyId == identity.CompanyId &&
                    x.TaxGroupId == dto.TaxGroupId);

            if (master == null)
                throw new Exception($"TaxGroupId {dto.TaxGroupId} not found.");

            // 2️⃣ Update master fields
            master.Code = dto.Code;
            master.Description = dto.Description;
            master.IsActive = true;
            master.CreatedBy = "Admin";
            master.CreatedOn = DateTime.UtcNow;

            //-----------------------------------------------
            // IGST CHILD HANDLING
            //-----------------------------------------------
            var incomingIgstIds = dto.TaxGroupIgstDetails?.Select(x => x.Id).ToList() ?? new List<int>();

            // 🔹 Soft delete those which are not in new list
            foreach (var old in master.TaxGroupIgstDetails.Where(x => x.IsActive))
            {
                if (!incomingIgstIds.Contains(old.Id))
                {
                    old.IsActive = false;
                    old.CreatedBy = "Admin";
                    old.CreatedOn = DateTime.UtcNow;
                }
            }

            // 🔹 Add / Update IGST
            if (dto.TaxGroupIgstDetails != null && dto.TaxGroupIgstDetails.Any())
            {
                foreach (var item in dto.TaxGroupIgstDetails)
                {
                    var existing = master.TaxGroupIgstDetails
                        .FirstOrDefault(x => x.Id == item.Id && x.IsActive);

                    if (existing != null)
                    {
                        // Update existing
                        existing.TaxId = item.TaxId;
                        existing.SerialNo = item.SerialNo;
                        existing.Rate = item.Rate ?? 0m;
                        existing.CreatedBy = "Admin";
                        existing.CreatedOn = DateTime.UtcNow;
                    }
                    else
                    {
                        // Insert new
                        var maxId = await _context.TaxGroupIgstDetails
                            .Where(x => x.TenantId == master.TenantId && x.CompanyId == master.CompanyId)
                            .MaxAsync(x => (int?)x.Id) ?? 0;

                        var newDetail = new TaxGroupIgstDetail
                        {
                            Id = maxId + 1,
                            TenantId = master.TenantId,
                            CompanyId = master.CompanyId,
                            TaxGroupId = master.TaxGroupId,
                            TaxId = item.TaxId,
                            SerialNo = item.SerialNo == 0 ? master.TaxGroupIgstDetails.Count + 1 : item.SerialNo,
                            Rate = item.Rate ?? 0m,
                            IsActive = true,
                            CreatedBy = "Admin",
                            CreatedOn = DateTime.UtcNow
                        };
                        master.TaxGroupIgstDetails.Add(newDetail);
                    }
                }
            }

            //-----------------------------------------------
            // SGST CHILD HANDLING
            //-----------------------------------------------
            var incomingSgstIds = dto.TaxGroupSgstDetails?.Select(x => x.Id).ToList() ?? new List<int>();

            // 🔹 Soft delete removed SGST rows
            foreach (var old in master.TaxGroupSgstDetails.Where(x => x.IsActive))
            {
                if (!incomingSgstIds.Contains(old.Id))
                {
                    old.IsActive = false;
                    old.CreatedBy = "Admin";
                    old.CreatedOn = DateTime.UtcNow;
                }
            }

            // 🔹 Add / Update SGST
            if (dto.TaxGroupSgstDetails != null && dto.TaxGroupSgstDetails.Any())
            {
                foreach (var item in dto.TaxGroupSgstDetails)
                {
                    var existing = master.TaxGroupSgstDetails
                        .FirstOrDefault(x => x.Id == item.Id && x.IsActive);

                    if (existing != null)
                    {
                        // Update existing
                        existing.TaxId = item.TaxId;
                        existing.SerialNo = item.SerialNo;
                        existing.Rate = item.Rate ?? 0m;
                        existing.CreatedBy = "Admin";
                        existing.CreatedOn = DateTime.UtcNow;
                    }
                    else
                    {
                        // Insert new
                        var maxId = await _context.TaxGroupSgstDetails
                            .Where(x => x.TenantId == master.TenantId && x.CompanyId == master.CompanyId)
                            .MaxAsync(x => (int?)x.Id) ?? 0;

                        var newDetail = new TaxGroupSgstDetail
                        {
                            Id = maxId + 1,
                            TenantId = master.TenantId,
                            CompanyId = master.CompanyId,
                            TaxGroupId = master.TaxGroupId,
                            TaxId = item.TaxId,
                            SerialNo = item.SerialNo == 0 ? master.TaxGroupSgstDetails.Count + 1 : item.SerialNo,
                            Rate = item.Rate ?? 0m,
                            IsActive = true,
                            CreatedBy = "Admin",
                            CreatedOn = DateTime.UtcNow
                        };
                        master.TaxGroupSgstDetails.Add(newDetail);
                    }
                }
            }

            //-----------------------------------------------
            // Save & Commit
            //-----------------------------------------------
            await _context.SaveChangesAsync();
            await trx.CommitAsync();

            return dto;
        }
        catch (Exception)
        {
            await trx.RollbackAsync();
            throw;
        }
    }

    public override async Task<PagedResult<IEnumerable<TaxGroupDto>>> GetAllAsync(QueryParameters queryParams)
    {
        try
        {
            var identity = _identityService.GetIdentity();

            // Temporary test/demo tenant
            identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            // ✅ Step 1: Build query with includes (specific to TaxGroup)
            var query = _context.Set<TaxGroup>()
                .AsNoTracking()
                .Include(tg => tg.TaxGroupIgstDetails)
                    .ThenInclude(ig => ig.Taxs)
                .Include(tg => tg.TaxGroupSgstDetails)
                    .ThenInclude(sg => sg.Taxs)
                .Where(x => x.TenantId == identity.TenantId &&
                            x.CompanyId == identity.CompanyId &&
                            x.IsActive == true);

            // ✅ Step 2: Apply filters and sorting dynamically
            query = query.ApplyFilters(queryParams.Filters)
                         .ApplySorting(queryParams.Sorts);

            // ✅ Step 3: Handle invalid pagination input
            if (queryParams.PageNo <= 0)
                queryParams.PageNo = 1;
            if (queryParams.PageSize <= 0)
                queryParams.PageSize = 100;

            // ✅ Step 4: Get total count
            var totalCount = await query.CountAsync();

            // ✅ Step 5: Get paged data
            var pagedData = await query
                .Skip((queryParams.PageNo - 1) * queryParams.PageSize)
                .Take(queryParams.PageSize)
                .ToListAsync();

            // ✅ Step 6: Map to DTO
            var dtoList = _mapper.Map<List<TaxGroupDto>>(pagedData);

            // ✅ Step 7: Return consistent paged result
            return new PagedResult<IEnumerable<TaxGroupDto>>
            {
                Data = dtoList,
                PageNo = queryParams.PageNo,
                PageSize = queryParams.PageSize,
                TotalCount = totalCount,
                Success = dtoList.Any(),
                Message = dtoList.Any() ? "Records fetched successfully." : "No records found."
            };
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, "Error fetching TaxGroup");
            return PagedResult<IEnumerable<TaxGroupDto>>.Fail($"Error fetching TaxGroup: {ex.Message}");
        }
    }

    public override async Task<IEnumerable<TaxGroupDto>> GetAllAsync()
    {
        try
        {
            var identity = _identityService.GetIdentity();

            // For testing/demo
            identity.TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            identity.CompanyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            // ✅ Include related tables dynamically
            var query = _context.Set<TaxGroup>()
                 .AsNoTracking()
                 .Include(tg => tg.TaxGroupIgstDetails)
                     .ThenInclude(ig => ig.Taxs)
                 .Include(tg => tg.TaxGroupSgstDetails)
                     .ThenInclude(sg => sg.Taxs)
                 .Where(x => x.TenantId == identity.TenantId &&
                             x.CompanyId == identity.CompanyId &&
                             x.IsActive == true);

            //var entity = await query.FirstOrDefaultAsync();

            if (query == null)
                throw new Exception($"Purchase Order not found ");

            // ✅ Map to DTO before returning
            var dto = _mapper.Map<IEnumerable<TaxGroupDto>>(query);
            return dto;
        }
        catch (Exception ex)
        {
            //Console.WriteLine($"[ItemService.GetByIdAsync] Error: {ex.Message}");
            throw;
        }

    }

}
