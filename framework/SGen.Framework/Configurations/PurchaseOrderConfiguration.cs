using Framework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Framework.Configurations
{
    public class PurchaseOrderConfiguration : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrder> entity)
        {
            entity.HasKey(e => new { e.TenantId, e.CompanyId, e.PoId });

            entity.ToTable("purchase_order");

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.PoId).HasColumnName("po_id");

            entity.Property(e => e.CreatedBy).HasColumnName("created_by")
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime").HasColumnName("created_on");
            entity.Property(e => e.DueAmount).HasColumnType("decimal(8, 2)").HasColumnName("due_amount");
            entity.Property(e => e.NetAmount).HasColumnType("decimal(8, 2)").HasColumnName("net_amount");
            entity.Property(e => e.PoDate).HasColumnType("datetime").HasColumnName("po_date");
            entity.Property(e => e.PoNo).HasMaxLength(50).HasColumnName("po_no");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Remarks).HasColumnName("remarks")
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RoundOff).HasColumnType("decimal(8, 2)").HasColumnName("round_off");
            entity.Property(e => e.FinYear).HasColumnName("fin_year")
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasColumnName("status")
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.TotalTaxableAmount).HasColumnType("decimal(8, 2)").HasColumnName("total_taxable_amount");
            entity.Property(e => e.TotalDiscount).HasColumnType("decimal(8, 2)").HasColumnName("total_discount");
            entity.Property(e => e.TotalTax).HasColumnType("decimal(8, 2)").HasColumnName("total_tax");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by")
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime").HasColumnName("updated_on");

            entity.HasOne(d => d.ProjectMasters).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.ProjectId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_po_order_project");

            entity.HasOne(d => d.SupplierMasters).WithMany(p => p.PurchaseOrders)
               .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.SupplierId })
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_purchase_order_supplier");
        }
    }
}
