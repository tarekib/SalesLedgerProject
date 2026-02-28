namespace SalesLedger.DataAccess.Entities
{
    public class SalesOrderLine
    {
        public int SalesOrderId { get; set; }
        
        public int ItemId { get; set; }
        
        public decimal Qty { get; set; }
        
        public decimal UnitPrice { get; set; }

        public virtual SalesOrder SalesOrder { get; set; }
        
        public virtual Item Item { get; set; }
    }
}
