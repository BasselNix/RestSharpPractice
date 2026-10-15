using System.Text.Json.Serialization;

namespace RestSharpPractice
{
    public class PizzaOrderForm
    {
        [JsonPropertyName("custname")]
        public string? CustName { get; set; }
        
        [JsonPropertyName("custtel")]
        public string? CustTel { get; set; }

        [JsonPropertyName("custemail")]
        public string? CustEmail { get; set; }
        
        public string? Delivery { get; set; }
        
        public string? Comments { get; set; }
        
        public string? Size { get; set; }
        
        public List<string> Topping { get; set; } = new();
    }
}
