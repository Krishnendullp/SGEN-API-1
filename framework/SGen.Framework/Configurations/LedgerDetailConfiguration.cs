using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGen.Framework.Entities;

namespace SGen.Framework.Configurations
{
    public class LedgerDetailConfiguration : IEntityTypeConfiguration<LedgerDetail>
    {
        public void Configure(EntityTypeBuilder<LedgerDetail> builder)
        {
            builder.ToTable("ledger_detail");

            // 🔑 Composite Primary Key
            builder.HasKey(x => new { x.TenantId, x.CompanyId, x.Id });

            // 🧱 Columns
            builder.Property(x => x.TenantId).HasColumnName("tenant_id");
            builder.Property(x => x.CompanyId).HasColumnName("company_id");
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.LedgerId).HasColumnName("ledger_id");
            builder.Property(x => x.SubLedgerId).HasColumnName("sub_ledger_id");
            builder.Property(x => x.EntryType).HasColumnName("entry_type").HasMaxLength(50);
            builder.Property(x => x.RefId).HasColumnName("ref_id");
            builder.Property(x => x.AccountType).HasColumnName("account_type").HasMaxLength(10);
            builder.Property(x => x.DebitAmount).HasColumnName("debit_amount").HasPrecision(18, 2);
            builder.Property(x => x.CreditAmount).HasColumnName("credit_amount").HasPrecision(18, 2);
            builder.Property(x => x.EntryDate).HasColumnName("entry_date");
            builder.Property(x => x.Narration).HasColumnName("narration").HasMaxLength(255);
            builder.Property(x => x.IsActive).HasColumnName("is_active").HasDefaultValue(true);

            builder.Property(x => x.CreatedOn).HasColumnName("created_on");
            builder.Property(x => x.CreatedBy).HasColumnName("created_by").HasMaxLength(50);
            builder.Property(x => x.UpdatedOn).HasColumnName("updated_on");
            builder.Property(x => x.UpdatedBy).HasColumnName("updated_by").HasMaxLength(50);

            // 🔗 Ledger FK (Composite)
            builder.HasOne(x => x.Ledger).WithMany()
                   .HasForeignKey(x => new { x.TenantId, x.CompanyId, x.LedgerId })
                   .HasConstraintName("fk_ledger_details_ledger")
                   .OnDelete(DeleteBehavior.Restrict);

            // 🔗 SubLedger FK (Composite)
            builder.HasOne(x => x.SubLedger).WithMany()
                   .HasForeignKey(x => new { x.TenantId, x.CompanyId, x.SubLedgerId })
                   .HasConstraintName("fk_ledger_details_subledger")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
