using Framework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Framework.Configurations
{
    public class TaxGroupSgstDetailConfiguration : IEntityTypeConfiguration<TaxGroupSgstDetail>
    {
        public void Configure(EntityTypeBuilder<TaxGroupSgstDetail> entity)
        {
            entity.HasKey(e => new { e.TenantId, e.CompanyId, e.Id });

            entity.ToTable("tax_group_sgst_detail");

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.CreatedBy).HasColumnName("created_by")
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime").HasColumnName("created_on");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.SerialNo).HasColumnName("serial_no");
            entity.Property(e => e.Rate).HasColumnType("decimal(6, 3)").HasColumnName("rate");
            entity.Property(e => e.TaxId).HasColumnName("tax_id");
            entity.Property(e => e.TaxGroupId).HasColumnName("tax_group_id");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by")
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime").HasColumnName("updated_on");
            entity.HasOne(d => d.Taxgroups).WithMany(p => p.TaxGroupSgstDetails)
                .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.TaxGroupId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tax_group_sgst_detail_group");

            entity.HasOne(d => d.Taxs).WithMany(p => p.TaxGroupSgstDetails)
                .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.TaxId })
                .HasConstraintName("FK_tax_group_sgst_tax");
        }
    }
}
