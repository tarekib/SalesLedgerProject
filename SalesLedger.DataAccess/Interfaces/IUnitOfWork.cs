using System;

namespace SalesLedger.DataAccess.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        IItemRepository Items { get; }
        ISalesOrderRepository SalesOrders { get; }
        IInvoiceRepository Invoices { get; }
        IRepository<Entities.GLTransaction> GLTransactions { get; }
        IRepository<Entities.Payment> Payments { get; }
        void Complete();
    }
}
