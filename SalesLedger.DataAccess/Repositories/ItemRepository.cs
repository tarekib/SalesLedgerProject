using SalesLedger.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SalesLedger.DataAccess.Repositories
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(SalesLedgerDbContext context) : base(context) { }

        public IEnumerable<Item> GetAllOrdered() => Context.Items.OrderBy(i => i.Name).ToList();
    }
}
