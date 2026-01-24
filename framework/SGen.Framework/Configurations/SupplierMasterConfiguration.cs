using Framework.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Framework.Configurations
{
    public class SupplierMasterConfiguration : IEntityTypeConfiguration<SupplierMaster>
    {
        public void Configure(EntityTypeBuilder<SupplierMaster> entity)
        {
            entity.HasKey(e => new { e.TenantId, e.CompanyId, e.SupplierId });

            entity.ToTable("supplier_master");

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

            entity.Property(e => e.LedgerId).HasColumnName("ledger_id");
            entity.Property(e => e.Address).HasMaxLength(300).HasColumnName("address");
            entity.Property(e => e.City).HasMaxLength(100).HasColumnName("city");
            entity.Property(e => e.ContactPerson).HasMaxLength(100).HasColumnName("contact_person");
            entity.Property(e => e.Country).HasMaxLength(100).HasColumnName("country");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by")
                .HasMaxLength(100)
                .HasDefaultValue("admin");
            entity.Property(e => e.CreatedOn).HasColumnName("created_on")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100).HasColumnName("email");
            entity.Property(e => e.GstNumber).HasMaxLength(20).HasColumnName("gst_number");
            entity.Property(e => e.IsActive).HasDefaultValue(true).HasColumnName("is_active");
            entity.Property(e => e.Phone).HasMaxLength(20).HasColumnName("phone");
            entity.Property(e => e.Pincode).HasMaxLength(10).HasColumnName("pincode");
            entity.Property(e => e.State).HasMaxLength(100).HasColumnName("state");
            entity.Property(e => e.SupplierCode).HasMaxLength(50).HasColumnName("supplier_code");
            entity.Property(e => e.SupplierName).HasMaxLength(200).HasColumnName("supplier_name");
            entity.Property(e => e.UpdatedBy).HasMaxLength(100).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime").HasColumnName("updated_on");

            entity.HasOne(x => x.LedgerMasters).WithMany(x => x.SupplierMasters)
             .HasForeignKey(x => new { x.TenantId, x.CompanyId, x.LedgerId })
             .HasConstraintName("fk_supplier_ledger");
        }
    }
}
