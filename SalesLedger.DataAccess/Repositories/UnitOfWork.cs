using SalesLedger.DataAccess.Entities;

namespace SalesLedger.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SalesLedgerDbContext _context;

        public UnitOfWork()
        {
            _context = new SalesLedgerDbContext();
            Customers = new CustomerRepository(_context);
            Items = new ItemRepository(_context);
            SalesOrders = new SalesOrderRepository(_context);
            Invoices = new InvoiceRepository(_context);
            GLTransactions = new Repository<GLTransaction>(_context);
            Payments = new Repository<Payment>(_context);
        }

        public ICustomerRepository Customers { get; }
       
        public IItemRepository Items { get; }
        
        public ISalesOrderRepository SalesOrders { get; }
        
        public IInvoiceRepository Invoices { get; }
        
        public IRepository<GLTransaction> GLTransactions { get; }
        
        public IRepository<Payment> Payments { get; }

        public void Complete() => _context.SaveChanges();

        public void Dispose() => _context.Dispose();
    }
}
