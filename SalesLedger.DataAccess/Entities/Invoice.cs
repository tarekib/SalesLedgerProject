using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesLedger.DataAccess.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
      
        public int SalesOrderId { get; set; }
        
        public int CustomerId { get; set; }
        
        public DateTime Date { get; set; }
        
        public decimal NetTotal { get; set; }
        
        public decimal TaxTotal { get; set; }
        
        public decimal GrossTotal { get; set; }

        public decimal AmountPaid { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }

        public virtual SalesOrder SalesOrder { get; set; }
        
        public virtual Customer Customer { get; set; }
        
        public virtual ICollection<InvoiceLine> Lines { get; set; } = new HashSet<InvoiceLine>();

        public virtual ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
    }
}
