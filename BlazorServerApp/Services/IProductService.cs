#nullable enable
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public interface IProductService
{
    Task<List<CategoryDto>> GetCategoriesAsync(CancellationToken cancellationToken = default);
    Task SaveProductAsync(ProductDto product, CancellationToken cancellationToken = default);
}