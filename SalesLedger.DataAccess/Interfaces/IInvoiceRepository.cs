using SalesLedger.DataAccess.Entities;
using System.Collections.Generic;

namespace SalesLedger.DataAccess.Repositories
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        IEnumerable<Invoice> GetAllWithDetails();
    }
}
