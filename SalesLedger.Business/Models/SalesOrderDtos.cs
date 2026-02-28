using System;
using System.Collections.Generic;

namespace SalesLedger.Business.Models
{
    public class CreateSalesOrderRequest
    {
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public List<SalesOrderLineDto> Lines { get; set; } = new List<SalesOrderLineDto>();
    }

    public class SalesOrderLineDto
    {
        public int ItemId { get; set; }
        public decimal Qty { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class SalesOrderListItem
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public int LineCount { get; set; }
    }

    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
