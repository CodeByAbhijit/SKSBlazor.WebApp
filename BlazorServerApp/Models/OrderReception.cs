using System.ComponentModel.DataAnnotations;

namespace BlazorServerApp.Models
{
    public class OrderReception
    {
        [Key]
        public int OrderReceptionId { get; set; }
        public int ProviderId { get; set; }
        public Provider? Provider { get; set; }
        public string ReceivedBy { get; set; } = "";
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public string Notes { get; set; } = "";
        public decimal FreightCharge { get; set; }
        public decimal SalesTaxRate { get; set; } // percentage (e.g., 5.0)
        public string Status { get; set; } = "RECEIVED";

        public List<OrderLine> Lines { get; set; } = new();
    }
}
