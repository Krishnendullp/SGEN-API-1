using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGen.Framework.Entities;


namespace SGen.Framework.Configurations
{
    public class SaleDetailTaxConfiguration : IEntityTypeConfiguration<SaleDetailTax>
    {
        public void Configure(EntityTypeBuilder<SaleDetailTax> entity)
        {
            // Primary Key (Composite)
            entity.HasKey(e => new { e.TenantId, e.CompanyId, e.Id });

            entity.ToTable("sale_detail_tax");

            // Columns
            entity.Property(e => e.TenantId).HasColumnName("tenant_id").HasColumnType("char(36)");
            entity.Property(e => e.CompanyId).HasColumnName("company_id").HasColumnType("char(36)");
            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.SaleDetailId).HasColumnName("sale_detail_id");
            entity.Property(e => e.TaxId).HasColumnName("tax_id");
            entity.Property(e => e.TaxPercentage).HasColumnName("tax_percentage").HasColumnType("decimal(10,2)");
            entity.Property(e => e.TaxAmount).HasColumnName("tax_amount").HasColumnType("decimal(18,2)");
            entity.Property(e => e.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            entity.Property(e => e.CreatedBy).HasColumnName("created_by").HasMaxLength(100).HasDefaultValue("admin");
            entity.Property(e => e.CreatedOn).HasColumnName("created_on").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by").HasMaxLength(100);
            entity.Property(e => e.UpdatedOn).HasColumnName("updated_on").HasColumnType("datetime");

            // ---------------------
            //   Foreign Keys
            // ---------------------

            // FK: sale_detail_tax → sale_master_detail (fk_stt_sale)
            entity.HasOne(d => d.SaleMasterDetails).WithMany(p => p.SaleDetailTaxs)
                .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.SaleDetailId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_stt_sale");

            // FK: sale_detail_tax → tax (fk_sale_tax)
            entity.HasOne(d => d.Taxs).WithMany(p => p.SaleMasterTaxs)
                .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.TaxId })
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_sale_tax");
        }
    }
}
