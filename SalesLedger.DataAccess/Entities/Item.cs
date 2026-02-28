using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesLedger.DataAccess.Entities
{
    public class Item
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public decimal UnitPrice { get; set; }
        
        public decimal OnHandQuantity { get; set; }

        public virtual ICollection<SalesOrderLine> SalesOrderLines { get; set; } = new HashSet<SalesOrderLine>();
        
        public virtual ICollection<InvoiceLine> InvoiceLines { get; set; } = new HashSet<InvoiceLine>();
    }
}
