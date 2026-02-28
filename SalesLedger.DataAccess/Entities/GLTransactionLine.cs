using System.ComponentModel.DataAnnotations;

namespace SalesLedger.DataAccess.Entities
{
    public class GLTransactionLine
    {
        public int Id { get; set; }
     
        public int GLTransactionId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Account { get; set; }

        public decimal Debit { get; set; }
        
        public decimal Credit { get; set; }

        public virtual GLTransaction GLTransaction { get; set; }
    }
}
