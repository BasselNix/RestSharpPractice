using RestSharp;

namespace RestSharpPractice
{
    public class Nghttp2ApiService
    {
        private readonly RestClient _client;

        public Nghttp2ApiService(string baseUrl = "https://nghttp2.org/httpbin/")
        {
            var options = new RestClientOptions(baseUrl)
            {
                Timeout = TimeSpan.FromSeconds(300)
            };

            _client = new RestClient(options);
            _client.AddDefaultHeader("User-Agent", "Learning Automation");
        }

        // Fetches the API specification from spec.json endpoint
        public async Task<SpecData?> GetSpecJsonAsync()
        {
            var request = new RestRequest("spec.json");

            try
            {
                return await _client.GetAsync<SpecData>(request);
            }
            catch (TimeoutException)
            {
                Console.WriteLine("The request timed out while contacting the gateway.");
                return null;
            }
        }
    }
}