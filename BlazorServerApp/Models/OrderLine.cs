using System.ComponentModel.DataAnnotations;

namespace BlazorServerApp.Models
{
    public class OrderLine
    {
        [Key]
        public int OrderLineId { get; set; }
        public int OrderReceptionId { get; set; }
        public OrderReception? OrderReception { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal => UnitPrice * Quantity;
    }
}
