using Framework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SGen.Framework.Configurations
{
    public class SupplierPaymentConfiguration : IEntityTypeConfiguration<SupplierPayment>
    {
        public void Configure(EntityTypeBuilder<SupplierPayment> entity)
        {
            entity.HasKey(e => new { e.TenantId, e.CompanyId, e.PaymentId });

            entity.ToTable("supplier_payment");

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.PaymentId).HasColumnName("payment_id");

            entity.Property(e => e.PoId).HasColumnName("po_id");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.Card).HasColumnType("decimal(18, 2)").HasColumnName("card");
            entity.Property(e => e.Cash).HasColumnType("decimal(18, 2)").HasColumnName("cash");
            entity.Property(e => e.Cheque).HasColumnType("decimal(18, 2)").HasColumnName("cheque");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by")
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnName("created_on")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.DeuAmount).HasColumnType("decimal(18, 2)").HasColumnName("deu_amount");
            entity.Property(e => e.IsActive).HasDefaultValue(true).HasColumnName("is_active");
            entity.Property(e => e.Neft).HasColumnType("decimal(18, 2)").HasColumnName("neft");
            entity.Property(e => e.PayAmount).HasColumnType("decimal(18, 2)").HasColumnName("pay_amount");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime").HasColumnName("payment_date");
            entity.Property(e => e.PaymentMode).HasMaxLength(50).HasColumnName("payment_mode");
            entity.Property(e => e.ReferenceNo).HasMaxLength(100).HasColumnName("reference_no");
            entity.Property(e => e.Remarks).HasMaxLength(255).HasColumnName("remarks");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)").HasColumnName("total_amount");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by")
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime").HasColumnName("updated_on");
            entity.Property(e => e.Upi).HasColumnType("decimal(18, 2)").HasColumnName("upi");

            entity.HasOne(d => d.PurchaseOrders).WithMany(p => p.SupplierPayments)
                .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.PoId })
                .HasConstraintName("FK_supplier_payment_purchase_order");

            entity.HasOne(d => d.SupplierMasters).WithMany(p => p.SupplierPayments)
                .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.SupplierId })
                .HasConstraintName("FK_supplier_payment_supplier_master");
        }
    }
}
