using System.ComponentModel.DataAnnotations;

namespace BlazorServerApp.Models
{
    public class Provider
    {
        [Key]
        public int ProviderId { get; set; }
        public string CompanyName { get; set; } = "";
        public string ContactFirstName { get; set; } = "";
        public string ContactLastName { get; set; } = "";
        public string Phone { get; set; } = "";
    }
}
