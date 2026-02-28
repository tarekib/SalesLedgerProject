using SalesLedger.DataAccess.Entities;
using System.Collections.Generic;

namespace SalesLedger.DataAccess.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IEnumerable<Customer> GetAllOrdered();
    }
}
