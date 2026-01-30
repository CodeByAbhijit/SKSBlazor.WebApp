#nullable enable
using System.ComponentModel.DataAnnotations;

public class ProductDto
{
    [Required]
    public string? ProductID { get; set; }

    [Required]
    public int? CategoryId { get; set; }

    [Required]
    public string? Unit { get; set; }

    [Range(0, double.MaxValue)]
    public decimal QuantityPerUnit { get; set; }
}

public class CategoryDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
}