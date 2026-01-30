#nullable enable
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using BlazorServerApp.Services;

public partial class OrdersApprovalBase : ComponentBase
{
    protected List<OrderViewModel> Orders { get; set; } = new();
    protected bool IsProcessing { get; set; }
    protected string? Message { get; set; }
    protected string MessageCss { get; set; } = "alert-info";

    [Inject] public IOrderService OrderService { get; set; } = default!;
    [Inject] public NavigationManager Nav { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        Orders = (await OrderService.GetPendingOrdersAsync()).Select(o => new OrderViewModel(o)).ToList();
    }

    protected async Task ApproveSelected()
    {
        IsProcessing = true;
        var selected = Orders.Where(x => x.Selected).Select(vm => vm.Id).ToList();
        if (!selected.Any())
        {
            Message = "No orders selected.";
            MessageCss = "alert-warning";
            IsProcessing = false;
            return;
        }

        try
        {
            await OrderService.ApproveOrdersAsync(selected);
            Message = "Selected orders approved.";
            MessageCss = "alert-success";
            // refresh
            Orders = (await OrderService.GetPendingOrdersAsync()).Select(o => new OrderViewModel(o)).ToList();
        }
        catch
        {
            Message = "Error approving orders.";
            MessageCss = "alert-danger";
        }
        finally
        {
            IsProcessing = false;
        }
    }

    protected void ShowInfo() => Message = "Select orders and click Approve to add stock.";

    protected void Close() => Nav.NavigateTo("/");
    protected void ToggleSelectAll()
    {
        var allChecked = Orders.All(x => x.Selected);
        foreach (var o in Orders) o.Selected = !allChecked;
    }
}

public class OrderViewModel
{
    public int Id { get; set; }
    public System.DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string? Status { get; set; }
    public bool Selected { get; set; }

    public OrderViewModel() { }
    public OrderViewModel(OrderDto dto)
    {
        Id = dto.Id;
        Date = dto.Date;
        Amount = dto.Amount;
        Status = dto.Status;
    }
}