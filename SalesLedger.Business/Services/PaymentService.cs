using SalesLedger.Business.Models;
using SalesLedger.DataAccess.Entities;
using SalesLedger.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesLedger.Business.Services
{
    public class PaymentService
    {
        private readonly Func<IUnitOfWork> _uowFactory;

        public PaymentService() : this(() => new UnitOfWork()) { }

        public PaymentService(Func<IUnitOfWork> uowFactory)
        {
            _uowFactory = uowFactory;
        }

        public List<PostedInvoiceDto> GetPayableInvoices()
        {
            using (var uow = _uowFactory())
            {
                return uow.Invoices.GetAllWithDetails()
                    .Where(x => x.Status == "Posted" || x.Status == "PartiallyPaid")
                    .Select(x => new PostedInvoiceDto
                    {
                        Id = x.Id,
                        CustomerName = x.Customer.Name,
                        CustomerId = x.CustomerId,
                        GrossTotal = x.GrossTotal,
                        AmountPaid = x.AmountPaid,
                        BalanceDue = x.GrossTotal - x.AmountPaid
                    })
                    .Where(x => x.BalanceDue > 0)
                    .ToList();
            }
        }

        /// <summary>
        /// Creates a payment against an invoice and posts the GL entry.
        /// Updates invoice status to Paid or PartiallyPaid.
        /// </summary>
        public int CreateAndPostPayment(int invoiceId, decimal amount, DateTime date)
        {
            if (invoiceId <= 0)
                throw new ArgumentException("Please select an invoice.");
            if (amount <= 0)
                throw new ArgumentException("Payment amount must be greater than zero.");

            using (var uow = _uowFactory())
            {
                var invoice = uow.Invoices.GetAllWithDetails()
                    .FirstOrDefault(x => x.Id == invoiceId);

                if (invoice == null)
                    throw new ArgumentException("Invoice not found.");
                if (invoice.Status != "Posted" && invoice.Status != "PartiallyPaid")
                    throw new ArgumentException("Only posted or partially paid invoices can receive payments.");

                decimal balanceDue = invoice.GrossTotal - invoice.AmountPaid;
                if (amount > balanceDue)
                    throw new ArgumentException(
                        string.Format("Payment amount ({0:N2}) exceeds the balance due ({1:N2}).", amount, balanceDue));

                
                var payment = new Payment
                {
                    CustomerId = invoice.CustomerId,
                    InvoiceId = invoiceId,
                    Date = date.Date,
                    Amount = amount
                };
                uow.Payments.Add(payment);

               
                invoice.AmountPaid += amount;
                if (invoice.AmountPaid >= invoice.GrossTotal)
                    invoice.Status = "Paid";
                else
                    invoice.Status = "PartiallyPaid";

                
                var glTransaction = new GLTransaction
                {
                    Date = date.Date
                };
                glTransaction.Lines.Add(new GLTransactionLine
                {
                    Account = "1000 Cash",
                    Debit = amount,
                    Credit = 0m
                });
                glTransaction.Lines.Add(new GLTransactionLine
                {
                    Account = "1100 AR",
                    Debit = 0m,
                    Credit = amount
                });
                uow.GLTransactions.Add(glTransaction);

                uow.Complete();
                return payment.Id;
            }
        }

  
        public List<PaymentListItem> GetAllPayments()
        {
            using (var uow = _uowFactory())
            {
                // Use Find on Invoices to get customer names via the invoice relationship
                var payments = uow.Payments.GetAll().Cast<Payment>().ToList();
                var invoiceIds = payments.Select(p => p.InvoiceId).Distinct().ToList();
                var invoices = uow.Invoices.GetAllWithDetails();

                return payments
                    .OrderByDescending(x => x.Date)
                    .ThenByDescending(x => x.Id)
                    .Select(x =>
                    {
                        var inv = invoices.FirstOrDefault(i => i.Id == x.InvoiceId);
                        return new PaymentListItem
                        {
                            Id = x.Id,
                            CustomerName = inv != null ? inv.Customer.Name : string.Empty,
                            InvoiceId = x.InvoiceId,
                            Date = x.Date,
                            Amount = x.Amount
                        };
                    })
                    .ToList();
            }
        }


        public List<InventoryItemDto> GetInventory()
        {
            using (var uow = _uowFactory())
            {
                return uow.Items.GetAllOrdered()
                    .Select(x => new InventoryItemDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UnitPrice = x.UnitPrice,
                        OnHandQuantity = x.OnHandQuantity
                    })
                    .ToList();
            }
        }


        public List<GLTransactionListItem> GetAllGLTransactions()
        {
            using (var uow = _uowFactory())
            {
                return uow.GLTransactions.GetAll()
                    .Cast<GLTransaction>()
                    .OrderByDescending(x => x.Date)
                    .ThenByDescending(x => x.Id)
                    .Select(x => new GLTransactionListItem
                    {
                        Id = x.Id,
                        Date = x.Date,
                        LineCount = x.Lines.Count,
                        TotalDebit = x.Lines.Sum(l => l.Debit),
                        TotalCredit = x.Lines.Sum(l => l.Credit)
                    })
                    .ToList();
            }
        }


        public List<GLTransactionDetailDto> GetGLTransactionLines(int transactionId)
        {
            using (var uow = _uowFactory())
            {
                var tx = uow.GLTransactions.GetAll()
                    .Cast<GLTransaction>()
                    .FirstOrDefault(x => x.Id == transactionId);

                if (tx == null)
                    return new List<GLTransactionDetailDto>();

                return tx.Lines.Select(l => new GLTransactionDetailDto
                {
                    Id = l.Id,
                    Date = tx.Date,
                    Account = l.Account,
                    Debit = l.Debit,
                    Credit = l.Credit
                }).ToList();
            }
        }
    }
}
