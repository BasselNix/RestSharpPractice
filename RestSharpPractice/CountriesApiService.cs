using RestSharp;

namespace RestSharpPractice
{
    public class CountriesApiService
    {
        private readonly RestClient _client;

        public CountriesApiService(string baseUrl = "https://countries.dev/")
        {
            var options = new RestClientOptions(baseUrl)
            {
                Timeout = TimeSpan.FromSeconds(300)
            };

            _client = new RestClient(options);
            _client.AddDefaultHeader("User-Agent", "Learning Automation");
        }

        // Fetches data of all countries from the /countries/ endpoint
        public async Task<List<CountryData>?> GetCountriesAsync()
        {
            var request = new RestRequest("countries/");
            try
            {
                return await _client.GetAsync<List<CountryData>>(request);
            }
            catch (TimeoutException)
            {
                Console.WriteLine("The request timed out while contacting the gateway.");
                return null;
            }
        }

        // Fetches language data from the /lang/ endpoint
        public async Task<List<CountryData>?> GetLanguageDataAsync(string languageCode)
        {
            var request = new RestRequest($"/lang/{languageCode}");
            try
            {
                return await _client.GetAsync<List<CountryData>>(request);
            }
            catch (TimeoutException)
            {
                Console.WriteLine("The request timed out while contacting the gateway.");
                return null;
            }
        }
    }
}
