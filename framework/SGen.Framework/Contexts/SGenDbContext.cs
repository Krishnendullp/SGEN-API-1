using Framework.Entities;
using Microsoft.EntityFrameworkCore;
using SGen.Framework.Entities;
using System;

namespace SGen.Framework.Contexts
{
    public class SGenDbContext : DbContext
    {
        public SGenDbContext(DbContextOptions<SGenDbContext> options) : base(options)
        {
        }

        //public DbSet<User> Users { get; set; }

        public virtual DbSet<CustomerMaster> CustomerMasters { get; set; }

        public virtual DbSet<ItemMaster> ItemMasters { get; set; }

        public virtual DbSet<LedgerCategory> LedgerCategorys { get; set; }

        public virtual DbSet<LedgerMaster> LedgerMasters { get; set; }

        public virtual DbSet<LedgerDetail> LedgerDetails { get; set; }

        public virtual DbSet<ProjectMaster> ProjectMasters { get; set; }

        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

        public virtual DbSet<SaleInvoicePayment> SaleInvoicePayments { get; set; }

        public virtual DbSet<SaleItemMaster> SaleItemMasters { get; set; }

        public virtual DbSet<SaleMaster> SaleMasters { get; set; }

        public virtual DbSet<SaleMasterDetail> SaleMasterDetails { get; set; }

        public virtual DbSet<SaleDetailTax> SaleDetailTaxs { get; set; }

        public virtual DbSet<SaleMasterTax> SaleMasterTaxs { get; set; }

        public virtual DbSet<SubLedger> SubLedgers { get; set; }

        public virtual DbSet<SupplierMaster> SupplierMasters { get; set; }

        public virtual DbSet<SupplierPayment> SupplierPayments { get; set; }

        public virtual DbSet<SupplierReturn> SupplierReturns { get; set; }

        public virtual DbSet<SupplierReturnDetail> SupplierReturnDetails { get; set; }

        public virtual DbSet<StoreTransaction> StoreTransactions { get; set; }

        public virtual DbSet<StoreTransactionTax> StoreTransactionTaxes { get; set; }

        public virtual DbSet<Tax> Taxes { get; set; }

        public virtual DbSet<TaxGroup> TaxGroups { get; set; }

        public virtual DbSet<TaxGroupIgstDetail> TaxGroupIgstDetails { get; set; }

        public virtual DbSet<TaxGroupSgstDetail> TaxGroupSgstDetails { get; set; }

        public virtual DbSet<Unit> Units { get; set; }

        public virtual DbSet<VoucherMaster> VoucherMasters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SGenDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
            // Fluent API config (optional)
        }

    }
}
