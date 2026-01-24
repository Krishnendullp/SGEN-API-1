using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGen.Framework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGen.Framework.Configurations
{
    public class CustomerMasterConfiguration : IEntityTypeConfiguration<CustomerMaster>
    {
        public void Configure(EntityTypeBuilder<CustomerMaster> entity)
        {
            entity.HasKey(e => new { e.TenantId, e.CompanyId, e.CustomerId });

            entity.ToTable("customer_master");

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");

            entity.Property(e => e.LedgerId).HasColumnName("ledger_Id");
            entity.Property(e => e.Address).HasMaxLength(300).HasColumnName("address");
            entity.Property(e => e.City).HasMaxLength(100).HasColumnName("city");
            entity.Property(e => e.ContactPerson).HasMaxLength(100).HasColumnName("contact_person");
            entity.Property(e => e.Country).HasMaxLength(100).HasColumnName("country");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by")
                .HasMaxLength(100)
                .HasDefaultValue("admin");
            entity.Property(e => e.CreatedOn).HasColumnName("created_on")
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Email).HasMaxLength(100).HasColumnName("email");
            entity.Property(e => e.GstNumber).HasMaxLength(20).HasColumnName("gst_number");
            entity.Property(e => e.IsActive).HasDefaultValue(true).HasColumnName("is_active");
            entity.Property(e => e.Phone).HasMaxLength(20).HasColumnName("phone");
            entity.Property(e => e.Pincode).HasMaxLength(10).HasColumnName("pincode");
            entity.Property(e => e.State).HasMaxLength(100).HasColumnName("state");
            entity.Property(e => e.CustomerCode).HasMaxLength(50).HasColumnName("customer_code");
            entity.Property(e => e.CustomerName).HasMaxLength(200).HasColumnName("customer_name");
            entity.Property(e => e.UpdatedBy).HasMaxLength(100).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime").HasColumnName("updated_on");

            entity.HasOne(x => x.LedgerMasters) .WithMany(x => x.CustomerMasters)
            .HasForeignKey(x => new { x.TenantId, x.CompanyId, x.LedgerId })
            .HasConstraintName("fk_customer_ledger");
        }
    }
}
