using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesLedger.DataAccess.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public virtual ICollection<SalesOrder> SalesOrders { get; set; } = new HashSet<SalesOrder>();
      
        public virtual ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();
        
        public virtual ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
    }
}
