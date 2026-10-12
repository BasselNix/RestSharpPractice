namespace RestSharpPractice
{
    public class PizzaOrderForm
    {
        public string? CustName { get; set; }
        public string? CustTel { get; set; }
        public string? CustEmail { get; set; }
        public string? Delivery { get; set; }
        public string? Comments { get; set; }
        public string? Size { get; set; }
        public List<string> Topping { get; set; } = new();
    }
}
