#nullable enable
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class ProductService : IProductService
{
    // In-memory store for POC
    private static readonly List<CategoryDto> _categories = new()
    {
        new CategoryDto { Id = 1, Name = "Seafood" },
        new CategoryDto { Id = 2, Name = "Beverages" },
    };

    public Task<List<CategoryDto>> GetCategoriesAsync(CancellationToken cancellationToken = default) =>
        Task.FromResult(new List<CategoryDto>(_categories));

    public Task SaveProductAsync(ProductDto product, CancellationToken cancellationToken = default)
    {
        // Simulate validation or business rules here.
        // For POC we accept any product that passes DataAnnotations.
        return Task.CompletedTask;
    }
}