using SalesLedger.DataAccess.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SalesLedger.DataAccess.Repositories
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(SalesLedgerDbContext context) : base(context) { }

        public IEnumerable<Invoice> GetAllWithDetails()
            => Context.Invoices
                .Include(x => x.Customer)
                .Include(x => x.SalesOrder)
                .Include(x => x.Lines)
                .Include(x => x.Lines.Select(l => l.Item))
                .OrderByDescending(x => x.Date)
                .ThenByDescending(x => x.Id)
                .ToList();
    }
}
