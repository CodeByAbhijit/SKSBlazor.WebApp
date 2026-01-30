#nullable enable
using BlazorServerApp.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorServerApp.Services
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetPendingOrdersAsync(CancellationToken cancellationToken = default);
        Task ApproveOrdersAsync(List<int> orderIds, CancellationToken cancellationToken = default);

        Task<List<Provider>> SearchProvidersAsync(string name);
        Task<List<Product>> SearchProductsAsync(string q);
        Task<OrderReception> CreateOrderDraftAsync(int providerId, string receivedBy);
        Task SaveOrderAsync(OrderReception order); // saves draft / reception (VB6 cmdSave)
        Task ApproveOrderAsync(int orderId, string approvedBy); // moves to stock (VB6 approve)
    }
}
