using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGen.Framework.Entities;
using System;


namespace SGen.Framework.Configurations
{
    public class SaleItemMasterConfiguration : IEntityTypeConfiguration<SaleItemMaster>
    {
        public void Configure(EntityTypeBuilder<SaleItemMaster> entity)
        {
            // Composite Primary Key
            entity.HasKey(e => new { e.TenantId, e.CompanyId, e.Id });

            entity.ToTable("sale_item_master");

            // Columns
            entity.Property(e => e.TenantId).HasColumnName("tenant_id").HasColumnType("char(36)");
            entity.Property(e => e.CompanyId).HasColumnName("company_id").HasColumnType("char(36)");
            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.TaxGroupId).HasColumnName("tax_group_id").IsRequired();
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
            entity.Property(e => e.Rate).HasColumnName("rate").HasColumnType("decimal(18,2)").HasDefaultValue(0.00m);
            entity.Property(e => e.CreatedBy).HasColumnName("created_by").HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnName("created_on") .HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by") .HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnName("updated_on").HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasColumnName("is_active").HasColumnType("tinyint(1)").HasDefaultValue(1).IsRequired();

            // 🔗 Foreign Key → tax_group
            entity.HasOne(d => d.TaxGroups).WithMany(p => p.SaleItemMasters)
                .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.TaxGroupId })
                .OnDelete(DeleteBehavior.Restrict)  // ✔ MySQL default
                .HasConstraintName("fk_sales_item_tax_group");
        }
    }
}
