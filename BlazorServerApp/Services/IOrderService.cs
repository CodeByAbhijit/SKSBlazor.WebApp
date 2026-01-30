#nullable enable
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public interface IOrderService
{
    Task<List<OrderDto>> GetPendingOrdersAsync(CancellationToken cancellationToken = default);
    Task ApproveOrdersAsync(List<int> orderIds, CancellationToken cancellationToken = default);
}