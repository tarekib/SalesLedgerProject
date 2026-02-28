using System;

namespace SalesLedger.Business.Models
{
    public class PaymentListItem
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public int InvoiceId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }

    public class PostedInvoiceDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
        public decimal GrossTotal { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal BalanceDue { get; set; }
    }

    public class InventoryItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal OnHandQuantity { get; set; }
    }

    public class GLTransactionListItem
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int LineCount { get; set; }
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
    }

    public class GLTransactionDetailDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Account { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }
}
