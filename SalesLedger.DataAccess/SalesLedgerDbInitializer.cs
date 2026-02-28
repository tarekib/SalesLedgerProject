using SalesLedger.DataAccess.Entities;
using System;
using System.Data.Entity;

namespace SalesLedger.DataAccess
{
    public class SalesLedgerDbInitializer : CreateDatabaseIfNotExists<SalesLedgerDbContext>
    {
        protected override void Seed(SalesLedgerDbContext context)
        {
            var customer1  = new Customer { Name = "Tarek Ibrahim" };
            var customer2 = new Customer { Name = "Test Customer" };
            context.Customers.Add(customer1);
            context.Customers.Add(customer2);
            context.SaveChanges();

            var itemA = new Item { Name = "Item A", UnitPrice = 100m, OnHandQuantity = 50m };
            var itemB = new Item { Name = "Item B", UnitPrice = 250m, OnHandQuantity = 20m };
            var itemC = new Item { Name = "Item C", UnitPrice = 75m, OnHandQuantity = 100m };
            context.Items.Add(itemA);
            context.Items.Add(itemB);
            context.Items.Add(itemC);
            context.SaveChanges();
            
            var so1 = new SalesOrder
            {
                CustomerId = customer1.Id,
                Date = DateTime.Today,
                Status = "Open"
            };
            so1.Lines.Add(new SalesOrderLine { ItemId = itemA.Id, Qty = 5m, UnitPrice = 100m });
            so1.Lines.Add(new SalesOrderLine { ItemId = itemB.Id, Qty = 2m, UnitPrice = 250m });
            context.SalesOrders.Add(so1);

            var so2 = new SalesOrder
            {
                CustomerId = customer2.Id,
                Date = DateTime.Today.AddDays(-1),
                Status = "Open"
            };
            so2.Lines.Add(new SalesOrderLine { ItemId = itemC.Id, Qty = 10m, UnitPrice = 75m });
            context.SalesOrders.Add(so2);

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
