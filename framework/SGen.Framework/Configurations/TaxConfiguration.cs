using Framework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SGen.DataAccess.Configurations
{
    public class TaxConfiguration : IEntityTypeConfiguration<Tax>
    {
        public void Configure(EntityTypeBuilder<Tax> entity)
        {
            entity.HasKey(e => new { e.TenantId, e.CompanyId, e.TaxId }).HasName("PK_taxs");

            entity.ToTable("tax");

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.TaxId).HasColumnName("tax_id");
            entity.Property(e => e.LedgerId).HasColumnName("ledger_id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50).HasColumnName("Created_by")
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime").HasColumnName("created_on");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50).HasColumnName("updated_by")
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime").HasColumnName("updated_on");

            entity.HasOne(e => e.LedgerMaster).WithMany(l => l.Taxes)
              .HasForeignKey(e => new { e.TenantId, e.CompanyId, e.LedgerId })
              .HasConstraintName("fk_tax_ledger_composite"); ;
        }
    }
}
