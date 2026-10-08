using RestSharp;

namespace RestSharpPractice
{
    internal class Nghttp2ApiService
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
            return await _client.GetAsync<SpecData>(request);
        }
    }
}
