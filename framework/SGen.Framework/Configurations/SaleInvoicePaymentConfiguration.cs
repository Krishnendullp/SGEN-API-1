using Framework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;


namespace SGen.Framework.Configurations
{
    public class SaleInvoicePaymentConfiguration : IEntityTypeConfiguration<SaleInvoicePayment>
    {
        public void Configure(EntityTypeBuilder<SaleInvoicePayment> entity)
        {
            // Composite Primary Key
            entity.HasKey(e => new { e.TenantId, e.CompanyId, e.InvoicePaymentId });

            entity.ToTable("sale_invoice_payment");

            // Columns
            entity.Property(e => e.TenantId).HasColumnName("tenant_id").HasColumnType("char(36)");
            entity.Property(e => e.CompanyId).HasColumnName("company_id").HasColumnType("char(36)");
            entity.Property(e => e.InvoicePaymentId).HasColumnName("invoice_payment_id");

            entity.Property(e => e.IsActive).HasColumnName("is_active").HasColumnType("bit") .HasDefaultValue(true);
            entity.Property(e => e.CreatedBy).HasColumnName("created_by").HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnName("created_on").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by").HasMaxLength(50).IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnName("updated_on").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Remarks).HasColumnName("remarks").HasMaxLength(50);
            entity.Property(e => e.PayAmount).HasColumnName("pay_amount").HasColumnType("decimal(18,2)");
            entity.Property(e => e.PayDate).HasColumnName("pay_date").HasColumnType("datetime");
            entity.Property(e => e.SaleId).HasColumnName("sale_id").IsRequired();
            entity.Property(e => e.PaymentMode).HasColumnName("payment_mode");

            // Foreign key → sale_master
            entity.HasOne(d => d.SaleMasters).WithMany(p => p.SaleInvoicePayments)
                .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.SaleId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_sale_master_invoice_payment");

        }
    }
}
