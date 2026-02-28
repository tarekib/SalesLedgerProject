using System;

namespace SalesLedger.DataAccess.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        IItemRepository Items { get; }
        ISalesOrderRepository SalesOrders { get; }
        void Complete();
    }
}
