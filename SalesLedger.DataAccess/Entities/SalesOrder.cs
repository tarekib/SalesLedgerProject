using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesLedger.DataAccess.Entities
{
    public class SalesOrder
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<SalesOrderLine> Lines { get; set; } = new HashSet<SalesOrderLine>();

        public virtual ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();
    }
}
