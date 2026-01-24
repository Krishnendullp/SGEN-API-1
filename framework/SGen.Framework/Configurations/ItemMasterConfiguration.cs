using Framework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Framework.Configurations
{
    public class ItemMasterConfiguration : IEntityTypeConfiguration<ItemMaster>
    {
        public void Configure(EntityTypeBuilder<ItemMaster> entity)
        {
            entity.HasKey(e => new { e.TenantId, e.CompanyId, e.ItemId });

            entity.ToTable("item_master");

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");

            entity.Property(e => e.CreatedBy).HasColumnName("created_by")
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime").HasColumnName("created_on");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.GstPercent).HasColumnType("decimal(5, 2)").HasColumnName("gst_percent");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name).HasMaxLength(200).HasColumnName("name");
            entity.Property(e => e.UnitId).HasColumnName("unit_id");
            entity.Property(e => e.TaxGroupId).HasColumnName("tax_group_id");
            entity.Property(e => e.HsnCode).HasMaxLength(20).HasColumnName("hsn_code");
            entity.Property(e => e.Rate).HasColumnType("decimal(6, 2)").HasColumnName("rate");
            entity.Property(e => e.UpdatedBy).HasMaxLength(100).HasColumnName("updated_by")
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime").HasColumnName("updated_on");

            entity.HasOne(d => d.TaxGroups).WithMany(p => p.ItemMasters)
                .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.TaxGroupId })
                .HasConstraintName("FK_item_master_tax_group");

            entity.HasOne(d => d.Units).WithMany(p => p.ItemMasterrs)
                .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.UnitId })
                .HasConstraintName("FK_item_master_unit");
        }
    }
}
