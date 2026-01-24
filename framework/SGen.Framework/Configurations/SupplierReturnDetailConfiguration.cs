using Framework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace SGen.Framework.Configurations
{
    public class SupplierReturnDetailConfiguration : IEntityTypeConfiguration<SupplierReturnDetail>
    {
        public void Configure(EntityTypeBuilder<SupplierReturnDetail> entity)
        {
            entity.HasKey(e => new { e.TenantId, e.CompanyId, e.ReturnDetailId }).HasName("pk_supplier_return_detail");

            entity.ToTable("supplier_return_detail");

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.ReturnDetailId).HasColumnName("return_detail_id");

            entity.Property(e => e.CreatedBy).HasColumnName("created_by")
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnName("created_on")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.IsActive).HasDefaultValue(true).HasColumnName("is_active");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)").HasColumnName("price");
            entity.Property(e => e.Qty).HasColumnType("decimal(18, 2)").HasColumnName("qty");
            entity.Property(e => e.PoId).HasColumnName("po_id");
            entity.Property(e => e.Remarks).HasColumnName("remarks")
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ReturnId).HasColumnName("return_id");
            entity.Property(x => x.Total).HasColumnType("decimal(18,2)")
                  .HasComputedColumnSql("`qty` * `price`", stored: true);
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by")
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime").HasColumnName("updated_on");

            entity.HasOne(d => d.ItemMasters).WithMany(p => p.SupplierReturnDetails)
                .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.ItemId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_return_detail_item");

            entity.HasOne(d => d.PurchaseOrders).WithMany(p => p.SupplierReturnDetails)
                .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.PoId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_return_detail_po");

            entity.HasOne(d => d.SupplierReturns).WithMany(p => p.SupplierReturnDetails)
                .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.ReturnId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_return_detail_master");
        }
    }
}
