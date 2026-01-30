#nullable enable
using BlazorServerApp.Data;
using BlazorServerApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorServerApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _db;
        public OrderService(AppDbContext db)
        {
            _db = db;
        }
        // In-memory seed
        private static readonly List<OrderDto> _orders = new()
    {
        new OrderDto { Id = 1, Date = DateTime.Today.AddDays(-2), Amount = 150.5m, Status = "Pending" },
        new OrderDto { Id = 2, Date = DateTime.Today.AddDays(-1), Amount = 250m, Status = "Pending" },
        new OrderDto { Id = 3, Date = DateTime.Today, Amount = 75.25m, Status = "Pending" },
    };

        public Task<List<OrderDto>> GetPendingOrdersAsync(CancellationToken cancellationToken = default)
        {
            var pending = _orders.Where(x => x.Status == "Pending").Select(x => new OrderDto { Id = x.Id, Date = x.Date, Amount = x.Amount, Status = x.Status }).ToList();
            return Task.FromResult(pending);
        }

        public Task ApproveOrdersAsync(List<int> orderIds, CancellationToken cancellationToken = default)
        {
            foreach (var id in orderIds)
            {
                var o = _orders.FirstOrDefault(x => x.Id == id);
                if (o != null) o.Status = "Approved";
            }
            return Task.CompletedTask;
        }

        public async Task<List<Provider>> SearchProvidersAsync(string name)
        {
            return await _db.Providers
                .Where(p => string.IsNullOrEmpty(name) ||
                            EF.Functions.Like(p.CompanyName, $"%{name}%") ||
                            EF.Functions.Like(p.ContactFirstName, $"%{name}%") ||
                            EF.Functions.Like(p.ContactLastName, $"%{name}%"))
                .ToListAsync();
        }

        public async Task<List<Product>> SearchProductsAsync(string q)
        {
            return await _db.Products
                .Where(p => string.IsNullOrEmpty(q) ||
                            EF.Functions.Like(p.Name, $"%{q}%") ||
                            EF.Functions.Like(p.Code, $"%{q}%"))
                .ToListAsync();
        }

        public async Task<OrderReception> CreateOrderDraftAsync(int providerId, string receivedBy)
        {
            var order = new OrderReception
            {
                ProviderId = providerId,
                ReceivedBy = receivedBy,
                OrderDate = DateTime.UtcNow,
                Status = "RECEIVED"
            };
            _db.OrderReceptions.Add(order);
            await _db.SaveChangesAsync();
            return order;
        }

        public async Task SaveOrderAsync(OrderReception order)
        {
            // Attach / update order and its lines
            if (order.OrderReceptionId == 0)
            {
                _db.OrderReceptions.Add(order);
            }
            else
            {
                _db.OrderReceptions.Update(order);
            }
            await _db.SaveChangesAsync();
        }

        public async Task ApproveOrderAsync(int orderId, string approvedBy)
        {
            var order = await _db.OrderReceptions
                .Include(o => o.Lines)
                .ThenInclude(l => l.Product)
                .FirstOrDefaultAsync(o => o.OrderReceptionId == orderId);

            if (order == null) throw new InvalidOperationException("Order not found");

            // Insert Stocks / update product stock - simplified model
            foreach (var line in order.Lines)
            {
                if (line.Product == null)
                    line.Product = await _db.Products.FindAsync(line.ProductId);

                if (line.Product != null)
                {
                    line.Product.UnitsInStock += line.Quantity;

                    // StockLog entry
                    var log = new StockLog
                    {
                        Date = DateTime.UtcNow,
                        DocId = order.OrderReceptionId,
                        DocType = "OrderReception",
                        ProductId = line.ProductId,
                        Quantity = line.Quantity,
                        StockPrice = line.LineTotal,
                        User = approvedBy ?? "system"
                    };
                    _db.StockLogs.Add(log);
                }
            }

            order.Status = "APPROVED";
            await _db.SaveChangesAsync();
        }
    }
}

