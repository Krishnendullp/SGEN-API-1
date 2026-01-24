using Framework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Framework.Configurations
{
    public class ProjectMasterConfiguration : IEntityTypeConfiguration<ProjectMaster>
    {
        public void Configure(EntityTypeBuilder<ProjectMaster> entity)
        {
            entity.HasKey(e => new { e.TenantId, e.CompanyId, e.ProjectId });

            entity.ToTable("project_master");

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)").HasColumnName("amount");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by")
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime").HasColumnName("created_on");
            entity.Property(e => e.Description).HasMaxLength(150).HasColumnName("description");
            entity.Property(e => e.LoaNo).HasMaxLength(50).HasColumnName("loa_no");
            entity.Property(e => e.Name).HasMaxLength(50).HasColumnName("name");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by")
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime").HasColumnName("updated_on");

        }
    }
}
