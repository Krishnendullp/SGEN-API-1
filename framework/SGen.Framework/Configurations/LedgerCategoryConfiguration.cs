using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SGen.Framework.Entities;

namespace SGen.Framework.Configurations
{
    public class LedgerCategoryConfiguration : IEntityTypeConfiguration<LedgerCategory>
    {
        public void Configure(EntityTypeBuilder<LedgerCategory> entity)
        {

            // Primary Key (Composite)
            entity.HasKey(x => new { x.TenantId, x.CompanyId, x.Id });

            entity.ToTable("ladger_category");

            entity.Property(x => x.TenantId).HasColumnName("tenant_id");
            entity.Property(x => x.CompanyId).HasColumnName("company_id");
            entity.Property(x => x.Id).HasColumnName("id");

            entity.Property(x => x.CategoryName).HasColumnName("category_name").HasMaxLength(50);
            entity.Property(x => x.CategoryCode).HasColumnName("category_code").HasMaxLength(10);
            entity.Property(x => x.SortOrder).HasColumnName("sort_order").HasDefaultValue(0);

            entity.Property(x => x.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            entity.Property(x => x.CreatedBy).HasColumnName("created_by").HasMaxLength(50);
            entity.Property(x => x.CreatedOn).HasColumnType("timestamp").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(x => x.UpdatedBy).HasColumnName("updated_by").HasMaxLength(50);
            entity.Property(x => x.UpdatedOn).HasColumnName("updated_on");

            // ===============================
            // ✅ HAS DATA (ROOT CATEGORIES)
            // ===============================

            var tenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var companyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            entity.HasData(
                new LedgerCategory
                {
                    TenantId = tenantId,
                    CompanyId = companyId,
                    Id = 1,
                    CategoryName = "Assets",
                    CategoryCode = "AST",
                    SortOrder = 1,
                    IsActive = true,
                    CreatedBy = "system",
                    UpdatedBy = "system"
                },
                new LedgerCategory
                {
                    TenantId = tenantId,
                    CompanyId = companyId,
                    Id = 2,
                    CategoryName = "Liabilities",
                    CategoryCode = "LIB",
                    SortOrder = 2,
                    IsActive = true,
                    CreatedBy = "system",
                    UpdatedBy = "system"
                },
                new LedgerCategory
                {
                    TenantId = tenantId,
                    CompanyId = companyId,
                    Id = 3,
                    CategoryName = "Income",
                    CategoryCode = "INC",
                    SortOrder = 3,
                    IsActive = true,
                    CreatedBy = "system",
                    UpdatedBy = "system"
                },
                new LedgerCategory
                {
                    TenantId = tenantId,
                    CompanyId = companyId,
                    Id = 4,
                    CategoryName = "Expenses",
                    CategoryCode = "EXP",
                    SortOrder = 4,
                    IsActive = true,
                    CreatedBy = "system",
                    UpdatedBy = "system"
                }
            );
        }
    }
}
