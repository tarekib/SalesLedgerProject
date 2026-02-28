using SalesLedger.Business.Models;
using SalesLedger.DataAccess.Entities;
using SalesLedger.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesLedger.Business.Services
{
    public class InvoiceService
    {
        private const decimal VatRate = 0.11m;

        private readonly Func<IUnitOfWork> _uowFactory;

        public InvoiceService() : this(() => new UnitOfWork()) { }

        public InvoiceService(Func<IUnitOfWork> uowFactory) => _uowFactory = uowFactory;

        public List<OpenSalesOrderDto> GetOpenSalesOrders()
        {
            using (var uow = _uowFactory())
            {
                return uow.SalesOrders.GetAllWithDetails()
                    .Where(x => x.Status == "Open")
                    .Select(x => new OpenSalesOrderDto
                    {
                        Id = x.Id,
                        CustomerName = x.Customer.Name,
                        Date = x.Date,
                        LineCount = x.Lines.Count
                    })
                    .ToList();
            }
        }

        public SalesOrderDetailDto GetSalesOrderDetail(int salesOrderId)
        {
            using (var uow = _uowFactory())
            {
                var orders = uow.SalesOrders.GetAllWithDetails();
                var order = orders.FirstOrDefault(x => x.Id == salesOrderId);

                if (order == null)
                    return null;

                return new SalesOrderDetailDto
                {
                    Id = order.Id,
                    CustomerId = order.CustomerId,
                    CustomerName = order.Customer.Name,
                    Date = order.Date,
                    Lines = order.Lines.Select(l => new SalesOrderLineDetailDto
                    {
                        ItemId = l.ItemId,
                        ItemName = l.Item != null ? l.Item.Name : string.Empty,
                        Qty = l.Qty,
                        UnitPrice = l.UnitPrice
                    }).ToList()
                };
            }
        }


        public int CreateInvoiceFromSalesOrder(int salesOrderId)
        {
            if (salesOrderId <= 0)
                throw new ArgumentException("Invalid sales order.");

            using (var uow = _uowFactory())
            {
                var orders = uow.SalesOrders.GetAllWithDetails();
                var order = orders.FirstOrDefault(x => x.Id == salesOrderId);

                if (order is null)
                    throw new ArgumentException("Sales order not found.");

                if (order.Status != "Open")
                    throw new ArgumentException("Only open sales orders can be released to an invoice.");

                if (order.Lines is null || order.Lines.Count == 0)
                    throw new ArgumentException("Sales order has no lines.");

                decimal netTotal = 0m;

                var invoice = new Invoice
                {
                    SalesOrderId = order.Id,
                    CustomerId = order.CustomerId,
                    Date = DateTime.Today,
                    Status = "Open"
                };

                foreach (var soLine in order.Lines)
                {
                    decimal lineTotal = soLine.Qty * soLine.UnitPrice;
                    decimal taxAmount = lineTotal * VatRate;
                    netTotal += lineTotal;

                    invoice.Lines.Add(new InvoiceLine
                    {
                        ItemId = soLine.ItemId,
                        Qty = soLine.Qty,
                        UnitPrice = soLine.UnitPrice,
                        LineTotal = lineTotal,
                        TaxAmount = taxAmount
                    });
                }

                invoice.NetTotal = netTotal;
                invoice.TaxTotal = netTotal * VatRate;
                invoice.GrossTotal = invoice.NetTotal + invoice.TaxTotal;

                // Mark the sales order as Released
                order.Status = "Released";

                uow.Invoices.Add(invoice);
                uow.Complete();

                return invoice.Id;
            }
        }

        public List<InvoiceListItem> GetAllInvoices()
        {
            using (var uow = _uowFactory())
            {
                return uow.Invoices.GetAllWithDetails()
                    .Select(x => new InvoiceListItem
                    {
                        Id = x.Id,
                        SalesOrderId = x.SalesOrderId,
                        CustomerName = x.Customer.Name,
                        Date = x.Date,
                        NetTotal = x.NetTotal,
                        TaxTotal = x.TaxTotal,
                        GrossTotal = x.GrossTotal,
                        AmountPaid = x.AmountPaid,
                        BalanceDue = x.GrossTotal - x.AmountPaid,
                        Status = x.Status,
                        LineCount = x.Lines.Count
                    })
                    .ToList();
            }
        }

        /// <summary>
        /// Posts an open invoice:
        /// Subtracts OnHandQuantity for each invoice line item.
        /// Creates a balanced GL Transaction:
        ///      Dr 1100 AR          = GrossTotal
        ///      Cr 4000 Sales       = NetTotal
        ///      Cr 2100 VAT Payable = TaxTotal
        /// Sets invoice status to "Posted".
        /// </summary>
        public void PostInvoice(int invoiceId)
        {
            if (invoiceId <= 0)
                throw new ArgumentException("Invalid invoice.");

            using (var uow = _uowFactory())
            {
                var invoices = uow.Invoices.GetAllWithDetails();
                var invoice = invoices.FirstOrDefault(x => x.Id == invoiceId);

                if (invoice == null)
                    throw new ArgumentException("Invoice not found.");
                if (invoice.Status != "Open")
                    throw new ArgumentException("Only open invoices can be posted.");
                if (invoice.Lines == null || invoice.Lines.Count == 0)
                    throw new ArgumentException("Invoice has no lines.");

                foreach (var line in invoice.Lines)
                {
                    if (line.Item == null)
                        throw new ArgumentException("Invoice line is missing item data.");

                    line.Item.OnHandQuantity -= line.Qty;
                }

                var glTransaction = new GLTransaction
                {
                    Date = DateTime.Today
                };

                glTransaction.Lines.Add(new GLTransactionLine
                {
                    Account = "1100 AR",
                    Debit = invoice.GrossTotal,
                    Credit = 0m
                });

                glTransaction.Lines.Add(new GLTransactionLine
                {
                    Account = "4000 Sales",
                    Debit = 0m,
                    Credit = invoice.NetTotal
                });

                glTransaction.Lines.Add(new GLTransactionLine
                {
                    Account = "2100 VAT Payable",
                    Debit = 0m,
                    Credit = invoice.TaxTotal
                });

                uow.GLTransactions.Add(glTransaction);

                invoice.Status = "Posted";

                uow.Complete();
            }
        }
    }
}
