using Framework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Framework.Configurations
{
    public class TaxGroupConfiguration : IEntityTypeConfiguration<TaxGroup>
    {
        public void Configure(EntityTypeBuilder<TaxGroup> entity)
        {

            entity.HasKey(e => new { e.TenantId, e.CompanyId, e.TaxGroupId });

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.TaxGroupId).HasColumnName("tax_group_id");

            entity.ToTable("tax_group");

            entity.Property(e => e.Code).HasMaxLength(50).HasColumnName("code");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by")
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime").HasColumnName("created_on");
            entity.Property(e => e.Description).HasMaxLength(50).HasColumnName("description"); ;
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by").HasColumnName("updated_by")
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime").HasColumnName("updated_on");
        }
    }
}
