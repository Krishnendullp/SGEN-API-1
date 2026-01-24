using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SGen.Framework.Entities;

namespace SGen.Framework.Configurations
{
    public class SubLedgerConfiguration : IEntityTypeConfiguration<SubLedger>
    {
        public void Configure(EntityTypeBuilder<SubLedger> entity)
        {

            // Primary Key (Composite)
            entity.HasKey(x => new { x.TenantId, x.CompanyId, x.SubLedgerId });

            entity.ToTable("sub_ledger");

            // Columns
            entity.Property(x => x.TenantId).HasColumnName("tenant_id").IsRequired();
            entity.Property(x => x.CompanyId).HasColumnName("company_id").IsRequired();
            entity.Property(x => x.SubLedgerId).HasColumnName("sub_ledger_id").IsRequired();

            entity.Property(x => x.LedgerId).HasColumnName("ledger_id").IsRequired(false);
            entity.Property(x => x.SubLedgerName).HasColumnName("sub_ledger_name").HasMaxLength(150);
            entity.Property(x => x.Mobile).HasColumnName("mobile").HasMaxLength(15);
            entity.Property(x => x.Address).HasColumnName("address").HasMaxLength(200);
            entity.Property(x => x.GstNo).HasColumnName("gst_no").HasMaxLength(20);

            entity.Property(x => x.IsActive).HasColumnName("is_active");
            entity.Property(x => x.CreatedBy).HasColumnName("created_by").HasMaxLength(50);
            entity.Property(x => x.CreatedOn).HasColumnName("created_on");
            entity.Property(x => x.UpdatedBy).HasColumnName("updated_by").HasMaxLength(50);
            entity.Property(x => x.UpdatedOn).HasColumnName("updated_on");

            // Foreign Key → ledger_master
            entity.HasOne(x => x.LedgerMaster).WithMany(x => x.SubLedgers)
                .HasForeignKey(x => new { x.TenantId, x.CompanyId, x.LedgerId })
                .HasConstraintName("fk_sub_ledger_ledger_master");
        }
    }

}
