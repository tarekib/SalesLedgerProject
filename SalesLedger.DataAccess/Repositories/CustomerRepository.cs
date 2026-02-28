using SalesLedger.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SalesLedger.DataAccess.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(SalesLedgerDbContext context) : base(context) { }

        public IEnumerable<Customer> GetAllOrdered()
            => Context.Customers.OrderBy(c => c.Name).ToList();
    }
}
