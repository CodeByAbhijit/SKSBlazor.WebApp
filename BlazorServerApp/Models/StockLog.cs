using System.ComponentModel.DataAnnotations;

namespace BlazorServerApp.Models
{
    public class StockLog
    {
        [Key]
        public int StockLogId { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public int DocId { get; set; }
        public string DocType { get; set; } = "OrderReception";
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int StockId { get; set; } // optional ref to stock row
        public decimal StockPrice { get; set; }
        public string User { get; set; } = "";
    }
}
