using SalesLedger.DataAccess.Entities;
using System.Collections.Generic;

namespace SalesLedger.DataAccess.Repositories
{
    public interface IItemRepository : IRepository<Item>
    {
        IEnumerable<Item> GetAllOrdered();
    }
}
