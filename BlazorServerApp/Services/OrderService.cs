#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;

public class OrderService : IOrderService
{
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
}