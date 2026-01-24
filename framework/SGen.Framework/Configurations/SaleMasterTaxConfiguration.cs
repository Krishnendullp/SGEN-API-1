using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGen.Framework.Entities;

namespace SGen.Framework.Configurations
{
    public class SaleMasterTaxConfiguration : IEntityTypeConfiguration<SaleMasterTax>
    {
        public void Configure(EntityTypeBuilder<SaleMasterTax> entity)
        {
            // Composite Primary Key
            entity.HasKey(x => new { x.TenantId, x.CompanyId, x.Id });

            entity.ToTable("sale_master_tax");

            // Columns
            entity.Property(x => x.TenantId).HasColumnName("tenant_id").HasColumnType("char(36)");
            entity.Property(x => x.CompanyId).HasColumnName("company_id").HasColumnType("char(36)");
            entity.Property(x => x.Id).HasColumnName("id");

            entity.Property(x => x.SaleId).HasColumnName("sale_id").IsRequired();
            entity.Property(x => x.TaxPercentage).HasColumnName("tax_percentage").HasColumnType("decimal(18,2)");
            entity.Property(x => x.TaxAmount).HasColumnName("tax_amount").HasColumnType("decimal(18,2)");
            entity.Property(x => x.IsActive).HasColumnName("is_active").HasColumnType("tinyint(1)").HasDefaultValue(1).IsRequired();
            entity.Property(x => x.CreatedBy).HasColumnName("created_by").HasMaxLength(100).IsRequired();
            entity.Property(x => x.CreatedOn).HasColumnName("created_on").HasColumnType("datetime").IsRequired();
            entity.Property(x => x.UpdatedBy).HasColumnName("updated_by").HasMaxLength(100);
            entity.Property(x => x.UpdatedOn).HasColumnName("updated_on").HasColumnType("datetime");

            // Foreign Key: sale_master_tax → sale_master
            entity.HasOne(x => x.SaleMasters).WithMany(x => x.SaleMasterTaxs)
                .HasForeignKey(x => new { x.TenantId, x.CompanyId, x.SaleId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_sale_master_tax_sale");
        }
    }
}
