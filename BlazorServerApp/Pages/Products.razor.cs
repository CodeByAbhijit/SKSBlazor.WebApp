#nullable enable
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using BlazorServerApp.Services;

public partial class ProductsBase : ComponentBase
{
    protected ProductDto Model { get; set; } = new();
    protected List<CategoryDto> Categories { get; set; } = new();
    protected bool IsProcessing { get; set; }
    protected string? Message { get; set; }
    protected string MessageCss { get; set; } = "alert-info";

    [Inject] public IProductService ProductService { get; set; } = default!;
    [Inject] public NavigationManager Nav { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        Categories = await ProductService.GetCategoriesAsync();
    }

    protected async Task SaveAsync()
    {
        IsProcessing = true;
        Message = null;
        try
        {
            await ProductService.SaveProductAsync(Model);
            Message = "Product saved";
            MessageCss = "alert-success";
        }
        catch
        {
            Message = "Error saving product";
            MessageCss = "alert-danger";
        }
        finally
        {
            IsProcessing = false;
        }
    }

    protected void Close() => Nav.NavigateTo("/");
}