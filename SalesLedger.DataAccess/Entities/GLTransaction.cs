using System;
using System.Collections.Generic;

namespace SalesLedger.DataAccess.Entities
{
    public class GLTransaction
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<GLTransactionLine> Lines { get; set; } = new HashSet<GLTransactionLine>();
    }
}
