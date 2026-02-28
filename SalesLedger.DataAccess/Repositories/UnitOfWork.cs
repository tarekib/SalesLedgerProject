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
        }

        public ICustomerRepository Customers { get; }
        public IItemRepository Items { get; }
        public ISalesOrderRepository SalesOrders { get; }

        public void Complete() => _context.SaveChanges();

        public void Dispose() => _context.Dispose();
    }
}
