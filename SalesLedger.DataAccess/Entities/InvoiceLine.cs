namespace SalesLedger.DataAccess.Entities
{
    public class InvoiceLine
    {
        public int Id { get; set; }

        public int InvoiceId { get; set; }

        public int ItemId { get; set; }

        public decimal Qty { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal LineTotal { get; set; }

        public decimal TaxAmount { get; set; }

        public virtual Invoice Invoice { get; set; }

        public virtual Item Item { get; set; }
    }
}
