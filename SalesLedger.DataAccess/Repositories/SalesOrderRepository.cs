using SalesLedger.DataAccess.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SalesLedger.DataAccess.Repositories
{
    public class SalesOrderRepository : Repository<SalesOrder>, ISalesOrderRepository
    {
        public SalesOrderRepository(SalesLedgerDbContext context) : base(context) { }

        public IEnumerable<SalesOrder> GetAllWithDetails()
            => Context.SalesOrders
                .Include(x => x.Customer)
                .Include(x => x.Lines)
                .OrderByDescending(x => x.Date)
                .ToList();
    }
}
