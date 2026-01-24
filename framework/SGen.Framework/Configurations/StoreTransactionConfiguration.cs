using Framework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Framework.Configurations
{
    public class StoreTransactionConfiguration : IEntityTypeConfiguration<StoreTransaction>
    {
        public void Configure(EntityTypeBuilder<StoreTransaction> entity)
        {
            entity.HasKey(e => new { e.TenantId, e.CompanyId, e.StoreId });

            entity.ToTable("store_transaction");

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.StoreId).HasColumnName("store_id");

            entity.Property(e => e.CreatedBy).HasColumnName("created_by")
                .HasMaxLength(100)
                .HasDefaultValue("admin");
            entity.Property(e => e.CreatedOn).HasColumnName("created_on")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true).HasColumnName("is_active");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.NetAmount).HasColumnType("decimal(18, 2)").HasColumnName("net_amount");
            entity.Property(e => e.PoId).HasColumnName("po_id");
            entity.Property(e => e.Remarks).HasMaxLength(50).HasColumnName("remarks");
            entity.Property(e => e.Rate).HasColumnType("decimal(18, 2)").HasColumnName("rate");
            entity.Property(e => e.TaxGroupId).HasColumnName("tax_group_id");
            entity.Property(e => e.TransactionDate).HasColumnType("datetime").HasColumnName("transaction_date");
            entity.Property(e => e.TransactionType).HasMaxLength(50).HasColumnName("transaction_type");
            entity.Property(e => e.UnitId).HasDefaultValue(1).HasColumnName("unit_id");
            entity.Property(e => e.UpdatedBy).HasMaxLength(100).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime").HasColumnName("updated_on");
            entity.Property(e => e.TotalTaxableAmount).HasColumnType("decimal(8, 2)").HasColumnName("total_taxable_amount");
            entity.Property(e => e.TotalTax).HasColumnType("decimal(8, 2)").HasColumnName("total_tax");

            entity.HasOne(d => d.ItemMaster).WithMany(p => p.StoreTransactions)
                .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.ItemId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_store_transaction_item_master");

            entity.HasOne(d => d.PurchaseOrder).WithMany(p => p.StoreTransactions)
                .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.PoId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_store_transaction_purchase_order");

            entity.HasOne(d => d.TaxGroup).WithMany(p => p.StoreTransactions)
                .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.TaxGroupId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_store_transaction_tax_group");

            entity.HasOne(d => d.units).WithMany(p => p.StoreTransactions)
                .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.UnitId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_store_transaction_unit");
        }
    }
}
