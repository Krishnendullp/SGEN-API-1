using Framework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace SGen.Framework.Configurations
{
    public class SupplierReturnConfigurations : IEntityTypeConfiguration<SupplierReturn>
    {
        public void Configure(EntityTypeBuilder<SupplierReturn> entity)
        {
            entity.HasKey(e => new { e.TenantId, e.CompanyId, e.ReturnId }).HasName("pk_supplier_return");

            entity.ToTable("supplier_return");

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.ReturnId).HasColumnName("return_id");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime").HasColumnName("created_on");
            entity.Property(e => e.IsActive).HasDefaultValue(true).HasColumnName("is_active");
            entity.Property(e => e.NetAmount).HasColumnType("decimal(18, 2)").HasColumnName("net_amount");
            entity.Property(e => e.Remarks).HasColumnName("remarks")
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ReturnDate).HasColumnName("return_date")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.ReturnNo).HasColumnName("return_no")
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RoundOff).HasColumnType("decimal(18, 2)").HasColumnName("round_off");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.Status).HasColumnName("status")
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Returned");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)").HasColumnName("total_amount");
            entity.Property(e => e.TotalDiscount).HasColumnType("decimal(18, 2)").HasColumnName("total_discount");
            entity.Property(e => e.TotalTax).HasColumnType("decimal(18, 2)").HasColumnName("total_tax");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by")
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime").HasColumnName("updated_on");

            entity.HasOne(d => d.ProjectMasters).WithMany(p => p.SupplierReturns)
                .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.ProjectId })
                .HasConstraintName("fk_supplier_return_project");

            entity.HasOne(d => d.SupplierMasters).WithMany(p => p.SupplierReturns)
                .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.SupplierId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_supplier_return_supplier");
        }
    }
}
