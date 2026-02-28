using SalesLedger.Business.Models;
using SalesLedger.DataAccess.Entities;
using SalesLedger.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesLedger.Business.Services
{
    public class SalesOrderService
    {
        private readonly Func<IUnitOfWork> _uowFactory;

        public SalesOrderService() : this(() => new UnitOfWork()) { }

        public SalesOrderService(Func<IUnitOfWork> uowFactory)
        {
            _uowFactory = uowFactory;
        }

        public int CreateSalesOrder(CreateSalesOrderRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");
            if (request.CustomerId <= 0)
                throw new ArgumentException("A customer must be selected.");
            if (request.Lines == null || request.Lines.Count == 0)
                throw new ArgumentException("At least one order line is required.");

            foreach (var line in request.Lines)
            {
                if (line.Qty <= 0)
                    throw new ArgumentException("Quantity must be greater than zero on all lines.");
                if (line.ItemId <= 0)
                    throw new ArgumentException("Each line must have an item selected.");
            }

            using (var uow = _uowFactory())
            {
                var order = new SalesOrder
                {
                    CustomerId = request.CustomerId,
                    Date = request.Date.Date,
                    Status = "Open"
                };

                foreach (var dto in request.Lines)
                {
                    order.Lines.Add(new SalesOrderLine
                    {
                        ItemId = dto.ItemId,
                        Qty = dto.Qty,
                        UnitPrice = dto.UnitPrice
                    });
                }

                uow.SalesOrders.Add(order);
                uow.Complete();
                return order.Id;
            }
        }

        public List<SalesOrderListItem> GetAllSalesOrders()
        {
            using (var uow = _uowFactory())
            {
                return uow.SalesOrders.GetAllWithDetails()
                    .Select(x => new SalesOrderListItem
                    {
                        Id = x.Id,
                        CustomerName = x.Customer.Name,
                        Date = x.Date,
                        Status = x.Status,
                        LineCount = x.Lines.Count
                    })
                    .ToList();
            }
        }

        public List<CustomerDto> GetCustomers()
        {
            using (var uow = _uowFactory())
            {
                return uow.Customers.GetAllOrdered()
                    .Select(x => new CustomerDto { Id = x.Id, Name = x.Name })
                    .ToList();
            }
        }

        public List<ItemDto> GetItems()
        {
            using (var uow = _uowFactory())
            {
                return uow.Items.GetAllOrdered()
                    .Select(x => new ItemDto { Id = x.Id, Name = x.Name, UnitPrice = x.UnitPrice })
                    .ToList();
            }
        }
    }
}
