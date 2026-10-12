namespace RestSharpPractice
{
    internal class Task2
    {
        public static Tuple<Dictionary<string, object>?, Dictionary<string, string>> ResponseFormAndHeaders(HttpbinPostResponse postData, Dictionary<string, string> headers)
        {
            return Tuple.Create(postData.Form, headers);
        }

        public static PizzaOrderForm MapToPizzaOrderForm(Dictionary<string, object>? formDict)
        {
            if (formDict == null) return new PizzaOrderForm();

            List<string> toppings = new();
            if (formDict.TryGetValue("topping", out var toppingValue) && toppingValue is System.Text.Json.JsonElement jsonElement)
            {
                toppings = jsonElement.EnumerateArray()
                    .Select(e => e.GetString() ?? string.Empty)
                    .ToList();
            }

            return new PizzaOrderForm
            {
                CustName = formDict.GetValueOrDefault("custname")?.ToString(),
                CustTel = formDict.GetValueOrDefault("custtel")?.ToString(),
                CustEmail = formDict.GetValueOrDefault("custemail")?.ToString(),
                Delivery = formDict.GetValueOrDefault("delivery")?.ToString(),
                Comments = formDict.GetValueOrDefault("comments")?.ToString(),
                Size = formDict.GetValueOrDefault("size")?.ToString(),
                Topping = toppings
            };
        }
    }
}
