using RestSharp;

namespace RestSharpPractice
{
    internal class HttpbinApiService
    {
        private readonly RestClient _client;

        public enum PizzaSize
        {
            Small,
            Medium,
            Large
        }

        public HttpbinApiService(string baseUrl = "https://httpbin.org/")
        {
            var options = new RestClientOptions(baseUrl)
            {
                Timeout = TimeSpan.FromSeconds(300)
            };

            _client = new RestClient(options);
            _client.AddDefaultHeader("User-Agent", "Learning Automation");
        }

        // Sends a POST request to the /post endpoint with form parameters.
        public async Task<(HttpbinPostResponse Data, Dictionary<string, string> Headers)> PostOrder(
            string custName,
            string custTel,
            string custEmail,
            string deliveryTime,
            string comments,
            PizzaSize size,
            List<string> toppings)
        {
            var request = new RestRequest("post", Method.Post);

            request.AddParameter("custname", custName);
            request.AddParameter("custtel", custTel);
            request.AddParameter("custemail", custEmail);
            request.AddParameter("delivery", deliveryTime);
            request.AddParameter("comments", comments);
            request.AddParameter("size", size.ToString().ToLower());

            foreach(var topping in toppings)
                request.AddParameter("topping", topping);

            var response = await _client.ExecuteAsync<HttpbinPostResponse>(request);

            if (!response.IsSuccessful || response.Data == null)
                throw new Exception($"POST request failed or Form data is null");

            // Combine both standard response headers and content headers
            var headers = response.Headers?
                .Concat(response.ContentHeaders ?? Enumerable.Empty<HeaderParameter>())
                .GroupBy(h => h.Name!, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(
                    g => g.Key,
                    g => string.Join(", ", g.Select(h => h.Value?.ToString()))
                ) ?? new Dictionary<string, string>();

            return (response.Data, headers);
        }
    }
}
