using System;
using System.Collections.Generic;

namespace SalesLedger.Business.Models
{
    public class InvoiceListItem
    {
        public int Id { get; set; }
        public int SalesOrderId { get; set; }
        public string CustomerName { get; set; }
        public DateTime Date { get; set; }
        public decimal NetTotal { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal GrossTotal { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal BalanceDue { get; set; }
        public string Status { get; set; }
        public int LineCount { get; set; }
    }

    public class OpenSalesOrderDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime Date { get; set; }
        public int LineCount { get; set; }
    }

    public class SalesOrderDetailDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime Date { get; set; }
        public List<SalesOrderLineDetailDto> Lines { get; set; } = new List<SalesOrderLineDetailDto>();
    }

    public class SalesOrderLineDetailDto
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Qty { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
