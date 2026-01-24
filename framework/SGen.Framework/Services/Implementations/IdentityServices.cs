using Framework.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SGen.Framework.Contexts;
using System.Security.Claims;
using System.Security.Principal;


namespace SGen.Framework.Services.Implementations
{
    public class IdentityServices : IIdentityServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SGenDbContext _context;

        public IdentityServices(IHttpContextAccessor httpContextAccessor, SGenDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        /// <summary>
        /// বর্তমান লগইন ইউজারের TenantId, CompanyId, UserId রিটার্ন করবে
        /// </summary>
        public IdentityModel GetIdentity()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity?.IsAuthenticated == true)
            {
                return new IdentityModel
                {
                    TenantId = Guid.Empty,
                    CompanyId = Guid.Empty,
                    UserId = null
                };
            }

            return new IdentityModel
            {
                TenantId = Guid.Parse(user.FindFirst("tenantId")?.Value ?? Guid.Empty.ToString()),
                CompanyId = Guid.Parse(user.FindFirst("companyId")?.Value ?? Guid.Empty.ToString()),
                UserId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
            };
        }

        /// <summary>
        /// Generic tenant-safe NextId generator for any entity
        /// Detects primary key via [Key], "Id" or "SomethingId"
        /// </summary>
        public async Task<int> GenerateNextPk<T>(Guid tenantId, Guid companyId) where T : class
        {
            int newId = 0;

            // EF Core থেকে entity type পাওয়া
            var entityType = _context.Model.FindEntityType(typeof(T))
                ?? throw new InvalidOperationException($"Entity type '{typeof(T).Name}' not found in DbContext.");

            // Composite primary key
            var keyProperties = entityType.FindPrimaryKey()?.Properties
                ?? throw new InvalidOperationException($"Entity type '{typeof(T).Name}' does not have a primary key.");

            if (keyProperties.Count != 3)
                throw new InvalidOperationException("Expected composite key with TenantId, CompanyId and a numeric PK.");

            // Numeric PK detect
            var pkProperty = keyProperties.FirstOrDefault(p => p.ClrType == typeof(int));
            if (pkProperty == null)
                throw new InvalidOperationException("No int PK found in composite key.");

            var pkName = pkProperty.Name;

            // Tenant + Company filter করে max id বের করা
            int lastId = await _context.Set<T>()
                .Where(x => EF.Property<Guid>(x, "TenantId") == tenantId &&
                            EF.Property<Guid>(x, "CompanyId") == companyId)
                .Select(x => EF.Property<int>(x, pkName))
                .OrderByDescending(id => id)
                .FirstOrDefaultAsync();

            // Next Id return
            return newId = lastId + 1;
        }
    }
    
}
