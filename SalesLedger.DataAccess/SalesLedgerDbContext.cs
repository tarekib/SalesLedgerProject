using SalesLedger.DataAccess.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace SalesLedger.DataAccess
{
    public class SalesLedgerDbContext : DbContext
    {
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

            modelBuilder.Entity<GLTransactionLine>()
                .HasRequired(x => x.GLTransaction)
                .WithMany(x => x.Lines)
                .HasForeignKey(x => x.GLTransactionId)
                .WillCascadeOnDelete(true);
        }
    }
}

namespace SalesLedger.DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SalesLedger.DataAccess.SalesLedgerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(SalesLedgerDbContext context)
        {
            context.Customers.AddOrUpdate(
                x => x.Name,
                new Customer { Name = "Acme Ltd" },
                new Customer { Name = "Blue Ocean LLC" });

            context.Items.AddOrUpdate(
                x => x.Name,
                new Item { Name = "Item A", UnitPrice = 100m, OnHandQuantity = 50m },
                new Item { Name = "Item B", UnitPrice = 250m, OnHandQuantity = 20m });

            context.SaveChanges();

            var customer = context.Customers.First(x => x.Name == "Acme Ltd");
            var item = context.Items.First(x => x.Name == "Item A");

            var order = context.SalesOrders.FirstOrDefault(x => x.CustomerId == customer.Id && x.Status == "Open");
            if (order == null)
            {
                order = context.SalesOrders.Add(new SalesOrder
                {
                    CustomerId = customer.Id,
                    Date = DateTime.Today,
                    Status = "Open"
                });
                context.SaveChanges();
            }

            context.SalesOrderLines.AddOrUpdate(
                x => new { x.SalesOrderId, x.ItemId },
                new SalesOrderLine
                {
                    SalesOrderId = order.Id,
                    ItemId = item.Id,
                    Qty = 2m,
                    UnitPrice = item.UnitPrice
                });

            context.SaveChanges();

            var invoice = context.Invoices.FirstOrDefault(x => x.SalesOrderId == order.Id);
            if (invoice == null)
            {
                invoice = context.Invoices.Add(new Invoice
                {
                    SalesOrderId = order.Id,
                    CustomerId = customer.Id,
                    Date = DateTime.Today,
                    NetTotal = 200m,
                    TaxTotal = 30m,
                    GrossTotal = 230m,
                    Status = "Posted"
                });
                context.SaveChanges();
            }

            if (!context.InvoiceLines.Any(x => x.InvoiceId == invoice.Id && x.ItemId == item.Id))
            {
                context.InvoiceLines.Add(new InvoiceLine
                {
                    InvoiceId = invoice.Id,
                    ItemId = item.Id,
                    Qty = 2m,
                    UnitPrice = 100m,
                    LineTotal = 200m,
                    TaxAmount = 30m
                });
            }

            if (!context.Payments.Any(x => x.CustomerId == customer.Id && x.Amount == 230m))
            {
                context.Payments.Add(new Payment
                {
                    CustomerId = customer.Id,
                    Date = DateTime.Today,
                    Amount = 230m
                });
            }

            var gl = context.GLTransactions.FirstOrDefault(x => x.Date == DateTime.Today);
            if (gl == null)
            {
                gl = context.GLTransactions.Add(new GLTransaction
                {
                    Date = DateTime.Today
                });
                context.SaveChanges();
            }

            if (!context.GLTransactionLines.Any(x => x.GLTransactionId == gl.Id && x.Account == "AccountsReceivable"))
            {
                context.GLTransactionLines.Add(new GLTransactionLine
                {
                    GLTransactionId = gl.Id,
                    Account = "AccountsReceivable",
                    Debit = 230m,
                    Credit = 0m
                });
            }

            if (!context.GLTransactionLines.Any(x => x.GLTransactionId == gl.Id && x.Account == "SalesRevenue"))
            {
                context.GLTransactionLines.Add(new GLTransactionLine
                {
                    GLTransactionId = gl.Id,
                    Account = "SalesRevenue",
                    Debit = 0m,
                    Credit = 200m
                });
            }

            if (!context.GLTransactionLines.Any(x => x.GLTransactionId == gl.Id && x.Account == "TaxPayable"))
            {
                context.GLTransactionLines.Add(new GLTransactionLine
                {
                    GLTransactionId = gl.Id,
                    Account = "TaxPayable",
                    Debit = 0m,
                    Credit = 30m
                });
            }

            context.SaveChanges();
        }
    }
}

