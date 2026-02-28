using System;

namespace SalesLedger.DataAccess.Entities
{
    public class Payment
    {
        public int Id { get; set; }
       
        public int CustomerId { get; set; }
        
        public int InvoiceId { get; set; }

        public DateTime Date { get; set; }
        
        public decimal Amount { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}
