using Framework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SGen.DataAccess.Configurations
{
    public class StoreTransactionTaxConfiguration : IEntityTypeConfiguration<StoreTransactionTax>
    {
        public void Configure(EntityTypeBuilder<StoreTransactionTax> entity)
        {
            entity.HasKey(e => new { e.TenantId, e.CompanyId, e.StoreTaxId }).HasName("PK_store_transaction_tax_1");

            entity.ToTable("store_transaction_tax");

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.StoreTaxId).HasColumnName("store_tax_id");

            entity.Property(e => e.CreatedBy).HasColumnName("created_by")
                .HasMaxLength(100)
                .HasDefaultValue("admin");
            entity.Property(e => e.CreatedOn).HasColumnName("created_on")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true).HasColumnName("is_active");
            entity.Property(e => e.StoreId).HasColumnName("store_id");
            entity.Property(e => e.TaxAmount).HasColumnType("decimal(18, 2)").HasColumnName("tax_amount");
            entity.Property(e => e.TaxPercentage).HasColumnType("decimal(10, 2)").HasColumnName("tax_percentage");
            entity.Property(e => e.TaxId).HasColumnName("tax_id");
            entity.Property(e => e.UpdatedBy).HasMaxLength(100).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime").HasColumnName("updated_on");

            entity.HasOne(d => d.StoreTransaction).WithMany(p => p.StoreTransactionTaxes)
                .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.StoreId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_store_transaction_tax_store_transaction");

            entity.HasOne(d => d.Taxs).WithMany(p => p.StoreTransactionTaxes)
                .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.TaxId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_store_transaction_tax_tax");
        }
    }
}
