using SalesLedger.DataAccess.Entities;
using System.Data.Entity;

namespace SalesLedger.DataAccess
{
    public class SalesLedgerDbContext : DbContext
    {
        static SalesLedgerDbContext()
        {
            Database.SetInitializer(new SalesLedgerDbInitializer());
        }

        public SalesLedgerDbContext() : base("name=SalesLedgerDb")
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<SalesOrder> SalesOrders { get; set; }

        public DbSet<SalesOrderLine> SalesOrderLines { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<InvoiceLine> InvoiceLines { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<GLTransaction> GLTransactions { get; set; }

        public DbSet<GLTransactionLine> GLTransactionLines { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SalesOrderLine>()
                .HasKey(x => new { x.SalesOrderId, x.ItemId });

            modelBuilder.Entity<Customer>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Item>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Item>()
                .Property(x => x.UnitPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Item>()
                .Property(x => x.OnHandQuantity)
                .HasPrecision(18, 2);

            modelBuilder.Entity<SalesOrder>()
                .Property(x => x.Status)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<SalesOrderLine>()
                .Property(x => x.Qty)
                .HasPrecision(18, 2);

            modelBuilder.Entity<SalesOrderLine>()
                .Property(x => x.UnitPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Invoice>()
                .Property(x => x.NetTotal)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Invoice>()
                .Property(x => x.TaxTotal)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Invoice>()
                .Property(x => x.GrossTotal)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Invoice>()
                .Property(x => x.AmountPaid)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Invoice>()
                .Property(x => x.Status)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<InvoiceLine>()
                .Property(x => x.Qty)
                .HasPrecision(18, 2);

            modelBuilder.Entity<InvoiceLine>()
                .Property(x => x.UnitPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<InvoiceLine>()
                .Property(x => x.LineTotal)
                .HasPrecision(18, 2);

            modelBuilder.Entity<InvoiceLine>()
                .Property(x => x.TaxAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Payment>()
                .Property(x => x.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<GLTransactionLine>()
                .Property(x => x.Account)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<GLTransactionLine>()
                .Property(x => x.Debit)
                .HasPrecision(18, 2);

            modelBuilder.Entity<GLTransactionLine>()
                .Property(x => x.Credit)
                .HasPrecision(18, 2);

            modelBuilder.Entity<SalesOrder>()
                .HasRequired(x => x.Customer)
                .WithMany(x => x.SalesOrders)
                .HasForeignKey(x => x.CustomerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SalesOrderLine>()
                .HasRequired(x => x.SalesOrder)
                .WithMany(x => x.Lines)
                .HasForeignKey(x => x.SalesOrderId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<SalesOrderLine>()
                .HasRequired(x => x.Item)
                .WithMany(x => x.SalesOrderLines)
                .HasForeignKey(x => x.ItemId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Invoice>()
                .HasRequired(x => x.SalesOrder)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => x.SalesOrderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Invoice>()
                .HasRequired(x => x.Customer)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => x.CustomerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<InvoiceLine>()
                .HasRequired(x => x.Invoice)
                .WithMany(x => x.Lines)
                .HasForeignKey(x => x.InvoiceId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<InvoiceLine>()
                .HasRequired(x => x.Item)
                .WithMany(x => x.InvoiceLines)
                .HasForeignKey(x => x.ItemId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Payment>()
                .HasRequired(x => x.Customer)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.CustomerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Payment>()
                .HasRequired(x => x.Invoice)
                .WithMany(x => x.Payments)
                .HasForeignKey(x => x.InvoiceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GLTransactionLine>()
                .HasRequired(x => x.GLTransaction)
                .WithMany(x => x.Lines)
                .HasForeignKey(x => x.GLTransactionId)
                .WillCascadeOnDelete(true);
        }
    }
}

