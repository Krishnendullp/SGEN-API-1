using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SGen.Framework.Entities;

namespace SGen.Framework.Configurations
{
    public class LedgerMasterConfiguration : IEntityTypeConfiguration<LedgerMaster>
    {
        public void Configure(EntityTypeBuilder<LedgerMaster> entity)
        {

            // Primary Key
            entity.HasKey(x => new { x.TenantId, x.CompanyId, x.LedgerId });

            entity.ToTable("ledger_master");

            // Column Mapping
            entity.Property(x => x.TenantId).HasColumnName("tenant_id");
            entity.Property(x => x.CompanyId).HasColumnName("company_id");
            entity.Property(x => x.LedgerId).HasColumnName("ledger_id");

            entity.Property(x => x.LedgerCateId).HasColumnName("ledger_cate_id");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(x => x.LedgerName).HasColumnName("ledger_name").HasMaxLength(150);
            entity.Property(e => e.IsControlLedger).HasColumnName("is_control_ledger").HasColumnType("tinyint(1)").HasDefaultValue(false);
            entity.Property(x => x.OpeningBalance).HasColumnName("opening_balance").HasColumnType("decimal(18,2)");
            entity.Property(x => x.OpeningType).HasColumnName("opening_type");
            entity.Property(x => x.DebitAmount).HasColumnName("debit_amount").HasColumnType("decimal(18,2)");
            entity.Property(x => x.CreditAmount).HasColumnName("credit_amount").HasColumnType("decimal(18,2)");
            entity.Property(x => x.GstNo).HasColumnName("gst_no").HasMaxLength(20);
            entity.Property(x => x.Mobile).HasColumnName("mobile").HasMaxLength(15);
            entity.Property(x => x.Address).HasColumnName("address").HasMaxLength(200);

            entity.Property(x => x.IsActive).HasColumnName("is_active");
            entity.Property(x => x.CreatedBy).HasColumnName("created_by").HasMaxLength(50);
            entity.Property(x => x.CreatedOn).HasColumnName("created_on");
            entity.Property(x => x.UpdatedBy).HasColumnName("updated_by").HasMaxLength(50);
            entity.Property(x => x.UpdatedOn).HasColumnName("updated_on");

            // Composite Foreign Key
            entity.HasOne(x => x.LedgerCategory).WithMany(x => x.LedgerMasters)
                .HasForeignKey(x => new { x.TenantId, x.CompanyId, x.LedgerCateId })
                .HasConstraintName("fk_ledger_master_ladger_category");

            // ===============================
            // ✅ HAS DATA (SEED)
            // ===============================

            var tenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var companyId = Guid.Parse("22222222-2222-2222-2222-222222222222");

            entity.HasData(
                new LedgerMaster
                {
                    TenantId = tenantId,
                    CompanyId = companyId,
                    LedgerId = 1,
                    LedgerCateId = 1,
                    ParentId = null,
                    LedgerName = "Current Assets",
                    IsControlLedger = true,
                    OpeningBalance = 0,
                    OpeningType = "Dr",
                    IsActive = true,
                    CreatedBy = "system"
                },
                new LedgerMaster
                {
                    TenantId = tenantId,
                    CompanyId = companyId,
                    LedgerId = 2,
                    LedgerCateId = 2,
                    ParentId = null,
                    LedgerName = "Current Liabilities",
                    IsControlLedger = true,
                    OpeningBalance = 0,
                    OpeningType = "Cr",
                    IsActive = true,
                    CreatedBy = "system"
                },
                new LedgerMaster
                {
                    TenantId = tenantId,
                    CompanyId = companyId,
                    LedgerId = 3,
                    LedgerCateId = 2,
                    ParentId = null,
                    LedgerName = "Purchase Account",
                    IsControlLedger = false,
                    OpeningBalance = 0,
                    OpeningType = "Dr",
                    IsActive = true,
                    CreatedBy = "system"
                },

                new LedgerMaster
                {
                    TenantId = tenantId,
                    CompanyId = companyId,
                    LedgerId = 4,
                    LedgerCateId = 2,
                    ParentId = 2,
                    LedgerName = "Account Payable",
                    IsControlLedger = true,
                    OpeningBalance = 0,
                    OpeningType = "Dr",
                    IsActive = true,
                    CreatedBy = "system"
                },
                new LedgerMaster
                {
                    TenantId = tenantId,
                    CompanyId = companyId,
                    LedgerId = 5,
                    LedgerCateId = 1,
                    ParentId = 1,
                    LedgerName = "Account Receivable",
                    IsControlLedger = true,
                    OpeningBalance = 0,
                    OpeningType = "Cr",
                    IsActive = true,
                    CreatedBy = "system"
                },
                new LedgerMaster
                {
                    TenantId = tenantId,
                    CompanyId = companyId,
                    LedgerId = 6,
                    LedgerCateId = 1,
                    ParentId = 1,
                    LedgerName = "Cash",
                    IsControlLedger = false,
                    OpeningBalance = 0,
                    OpeningType = "Dr",
                    IsActive = true,
                    CreatedBy = "system"
                },
                new LedgerMaster
                {
                     TenantId = tenantId,
                     CompanyId = companyId,
                     LedgerId = 7,
                     LedgerCateId = 1,
                     ParentId = 1,
                     LedgerName = "Bank",
                     IsControlLedger = false,
                     OpeningBalance = 0,
                     OpeningType = "Dr",
                     IsActive = true,
                     CreatedBy = "system"
                },
                new LedgerMaster
                {
                    TenantId = tenantId,
                    CompanyId = companyId,
                    LedgerId = 8,
                    LedgerCateId = 1,
                    ParentId = 1,
                    LedgerName = "Upi",
                    IsControlLedger = false,
                    OpeningBalance = 0,
                    OpeningType = "Dr",
                    IsActive = true,
                    CreatedBy = "system"
                },
                new LedgerMaster
                {
                    TenantId = tenantId,
                    CompanyId = companyId,
                    LedgerId = 9,
                    LedgerCateId = 1,
                    ParentId = 1,
                    LedgerName = "Cheque-in-hand",
                    IsControlLedger = false,
                    OpeningBalance = 0,
                    OpeningType = "Dr",
                    IsActive = true,
                    CreatedBy = "system"
                },
                new LedgerMaster
                {
                    TenantId = tenantId,
                    CompanyId = companyId,
                    LedgerId = 10,
                    LedgerCateId = 2,
                    ParentId = 2,
                    LedgerName = "Sundry Creditors",
                    IsControlLedger = true,
                    OpeningBalance = 0,
                    OpeningType = "Cr",
                    IsActive = true,
                    CreatedBy = "system"
                },
                new LedgerMaster
                {
                    TenantId = tenantId,
                    CompanyId = companyId,
                    LedgerId = 11,
                    LedgerCateId = 2,
                    ParentId = 2,
                    LedgerName = "Bills Payable",
                    IsControlLedger = false,
                    OpeningBalance = 0,
                    OpeningType = "Cr",
                    IsActive = true,
                    CreatedBy = "system"
                },
                new LedgerMaster
                {
                    TenantId = tenantId,
                    CompanyId = companyId,
                    LedgerId = 12,
                    LedgerCateId = 2,
                    ParentId = 2,
                    LedgerName = "Outstanding Expenses",
                    IsControlLedger = true,
                    OpeningBalance = 0,
                    OpeningType = "Cr",
                    IsActive = true,
                    CreatedBy = "system"
                },

                new LedgerMaster
                {
                    TenantId = tenantId,
                    CompanyId = companyId,
                    LedgerId = 13,
                    LedgerCateId = 2,
                    ParentId = 2,
                    LedgerName = "Statutory Payables",
                    IsControlLedger = true,
                    OpeningBalance = 0,
                    OpeningType = "Cr",
                    IsActive = true,
                    CreatedBy = "system"
                },
                new LedgerMaster
                {
                    TenantId = tenantId,
                    CompanyId = companyId,
                    LedgerId = 14,
                    LedgerCateId = 2,
                    ParentId = 12,
                    LedgerName = "GST Payable",
                    IsControlLedger = true,
                    OpeningBalance = 0,
                    OpeningType = "Cr",
                    IsActive = true,
                    CreatedBy = "system"
                },
                new LedgerMaster
                {
                    TenantId = tenantId,
                    CompanyId = companyId,
                    LedgerId = 15,
                    LedgerCateId = 2,
                    ParentId = 12,
                    LedgerName = "TDS Payable",
                    IsControlLedger = true,
                    OpeningBalance = 0,
                    OpeningType = "Cr",
                    IsActive = true,
                    CreatedBy = "system"
                },
                new LedgerMaster
                {
                    TenantId = tenantId,
                    CompanyId = companyId,
                    LedgerId = 16,
                    LedgerCateId = 2,
                    ParentId = 12,
                    LedgerName = "PF / ESI Payable",
                    IsControlLedger = false,
                    OpeningBalance = 0,
                    OpeningType = "Cr",
                    IsActive = true,
                    CreatedBy = "system"
                }

            );
        
        }
    }

}
