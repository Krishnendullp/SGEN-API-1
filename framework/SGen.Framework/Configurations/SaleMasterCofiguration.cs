using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGen.Framework.Entities;
using System;

namespace SGen.Framework.Configurations
{
    public class SaleMasterCofiguration : IEntityTypeConfiguration<SaleMaster>
    {
        public void Configure(EntityTypeBuilder<SaleMaster> entity)
        {
            // Primary Key (Composite)
            entity.HasKey(e => new { e.TenantId, e.CompanyId, e.Id });
            entity.ToTable("sale_master");

            entity.Property(e => e.TenantId).HasColumnName("tenant_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.TaxableAmount).HasColumnName("taxable_amount").HasColumnType("decimal(18,2)").HasDefaultValue(0.00m);
            entity.Property(e => e.TotalTax).HasColumnName("total_tax").HasColumnType("decimal(18,2)").HasDefaultValue(0.00m);
            entity.Property(e => e.TotalAmount).HasColumnName("total_amount").HasColumnType("decimal(18,2)").HasDefaultValue(0.00m);
            entity.Property(e => e.DiscountAmount).HasColumnName("discount_amount").HasColumnType("decimal(18,2)").HasDefaultValue(0.00m);

            entity.Property(e => e.SaleDate).HasColumnName("sale_date").HasColumnType("datetime").IsRequired();
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by").HasMaxLength(100);
            entity.Property(e => e.CreatedOn).HasColumnName("created_on").HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by").HasMaxLength(100);
            entity.Property(e => e.UpdatedOn).HasColumnName("updated_on").HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasColumnName("is_active").HasDefaultValue(true);
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.InvNo).HasColumnName("inv_no").HasMaxLength(50);
            entity.Property(e => e.FinYr).HasColumnName("fin_yr").HasMaxLength(10);
            entity.Property(e => e.PaymentReceiveAmount).HasColumnName("payment_receive_amount").HasColumnType("decimal(18,2)").HasDefaultValue(0.00m);
            entity.Property(e => e.CustomerNote).HasColumnName("customer_note").HasMaxLength(100);
            entity.Property(e => e.Discount).HasColumnName("discount").HasMaxLength(100); // optional separate
            entity.Property(e => e.ItemTax).HasColumnName("item_tax").HasColumnType("decimal(18,2)").HasDefaultValue(0.00m);
            entity.Property(e => e.NetAmount).HasColumnName("net_amount").HasColumnType("decimal(18,2)").HasDefaultValue(0.00m);
            entity.Property(e => e.TaxGroupId).HasColumnName("tax_group_id");
            entity.Property(e => e.Status).HasColumnName("status").HasMaxLength(50);

            // Foreign Keys
            entity.HasOne(d => d.CustomerMasters).WithMany(p => p.SaleMasters)
                  .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.CustomerId })
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasConstraintName("fk_sale_customer");

            entity.HasOne(d => d.ProjectMasters).WithMany(p => p.SaleMasters)
                  .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.ProjectId })
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasConstraintName("fk_sale_master_project");

            entity.HasOne(d => d.TaxGroups).WithMany(p => p.SaleMasters)
                  .HasForeignKey(d => new { d.TenantId, d.CompanyId, d.TaxGroupId })
                  .OnDelete(DeleteBehavior.Cascade)
                  .HasConstraintName("fk_sale_master_tax_group");

        }
    }
}
