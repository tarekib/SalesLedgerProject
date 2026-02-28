using SalesLedger.DataAccess.Entities;
using System.Collections.Generic;

namespace SalesLedger.DataAccess.Repositories
{
    public interface ISalesOrderRepository : IRepository<SalesOrder>
    {
        IEnumerable<SalesOrder> GetAllWithDetails();
    }
}
