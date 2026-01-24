using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGen.Framework.Entities;
using System;

namespace SGen.Framework.Configurations
{
    partial class SaleMasterDetailConfiguration : IEntityTypeConfiguration<SaleMasterDetail>
    {
        public void Configure(EntityTypeBuilder<SaleMasterDetail> entity)
        {
            entity.HasKey(e => new { e.TenantId, e.CompanyId, e.Id });

            entity.ToTable("sale_master_detail");

            // Columns
            entity.Property(e => e.TenantId).HasColumnName("tenant_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.SaleId).HasColumnName("sale_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity").HasColumnType("decimal(18,2)").HasDefaultValue(1.00m);
            entity.Property(e => e.TaxGroupId).HasColumnName("tax_group_id");
            entity.Property(e => e.TaxableAmount).HasColumnName("taxable_amount").HasColumnType("decimal(18,2)").HasDefaultValue(0.00m);
            entity.Property(e => e.TotalTax).HasColumnName("total_tax").HasColumnType("decimal(18,2)").HasDefaultValue(0.00m);
            entity.Property(e => e.Total).HasColumnName("total").HasColumnType("decimal(18,2)").HasDefaultValue(0.00m);
            entity.Property(e => e.CreatedBy).HasColumnName("created_by").HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnName("created_on").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by").HasMaxLength(100).IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnName("updated_on").HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            entity.Property(e => e.Rate).HasColumnName("rate").HasColumnType("decimal(18,2)").HasDefaultValue(0.00m);

            // ----------------------
            //  🔗 Foreign Keys
            // ----------------------

            // FK → sale_master
            entity.HasOne(d => d.SaleMasters).WithMany(p => p.SaleMasterDetails)
                .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.SaleId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_sale_details_sale");

            // FK → sale_item_master
            entity.HasOne(d => d.SaleItemMasters).WithMany(p => p.SaleMasterDetails)
                .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.ItemId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_sale_details_item");

            // FK → tax_group
            entity.HasOne(d => d.TaxGroups).WithMany(p => p.SaleMasterDetails)
                .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.TaxGroupId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_sale_detail_tax_group");

        }
    }
}
