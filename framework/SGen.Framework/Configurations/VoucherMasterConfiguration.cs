using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGen.Framework.Entities;

namespace SGen.Framework.Configurations
{
    public class VoucherMasterConfiguration : IEntityTypeConfiguration<VoucherMaster>
    {
        public void Configure(EntityTypeBuilder<VoucherMaster> entity)
        {
            entity.ToTable("voucher_master");

            entity.HasKey(x => new { x.TenantId, x.CompanyId, x.VoucherId });

            entity.Property(x => x.TenantId).HasColumnName("tenant_id");
            entity.Property(x => x.CompanyId).HasColumnName("company_id");
            entity.Property(x => x.VoucherId).HasColumnName("voucher_id");
 
            entity.Property(x => x.VoucherNo).HasColumnName("voucher_no");
            entity.Property(x => x.VoucherType).HasColumnName("voucher_type");
            entity.Property(x => x.VoucherDate).HasColumnName("voucher_date");
            entity.Property(x => x.ReferenceNo).HasColumnName("reference_no").HasMaxLength(100);
            entity.Property(x => x.Narration).HasColumnName("narration").HasMaxLength(100);

            entity.Property(x => x.IsPosted).HasColumnName("is_posted").HasDefaultValue(false);
            entity.Property(x => x.IsCancelled).HasColumnName("is_cancelled").HasDefaultValue(false);

            entity.Property(x => x.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            entity.Property(x => x.CreatedBy).HasColumnName("created_by").HasMaxLength(50);
            entity.Property(x => x.CreatedOn).HasColumnType("timestamp").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(x => x.UpdatedBy).HasColumnName("updated_by").HasMaxLength(50);
            entity.Property(x => x.UpdatedOn).HasColumnName("updated_on");
        }
    }
}
