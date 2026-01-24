using Framework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Framework.Configurations
{
    public class UnitConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> entity)
        {
            entity.HasKey(e => new { e.TenantId, e.CompanyId, e.UnitId });

            entity.ToTable("unit");

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.UnitId).HasColumnName("unit_id");

            entity.Property(e => e.ConversionValue).HasColumnType("decimal(6, 3)").HasColumnName("conversion_value");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by")
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime").HasColumnName("created_on");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Description).HasMaxLength(100).HasColumnName("description");
            entity.Property(e => e.UnitName).HasMaxLength(50).HasColumnName("unit_name");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by")
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime").HasColumnName("updated_on");
        }
    }
}
