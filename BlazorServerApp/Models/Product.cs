using System.ComponentModel.DataAnnotations;

namespace BlazorServerApp.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public int QuantityPerUnit { get; set; }

    }
}
