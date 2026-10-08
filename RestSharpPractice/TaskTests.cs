using NUnit.Framework;
using static RestSharpPractice.HttpbinApiService;

namespace RestSharpPractice.Tests
{
	[TestFixture]
	public class Task1Tests
	{
		private Nghttp2ApiService _apiService;

		[SetUp]
		public void Setup()
		{
			_apiService = new Nghttp2ApiService();
		}

		[Test]
		public async Task GetSpecJson_ReturnsValidPathsAndResponses()
		{
			var spec = await _apiService.GetSpecJsonAsync();

			Assert.That(spec, Is.Not.Null, "Response from spec.json should not be null");
			Assert.That(spec.Paths, Is.Not.Null.And.Not.Empty, "Paths dictionary should contain entries");
		}

		[Test]
		public async Task Task1_UrlPathsWithResponseCodesNot200_FiltersPathsWithout200()
		{
			var spec = await _apiService.GetSpecJsonAsync();
			Assert.That(spec, Is.Not.Null);

			var result = Task1.UrlPathsWithResponseCodesNot200(spec);

			Assert.That(result, Is.Not.Null);

			// Validate that NONE of the returned paths contain status code 200
			foreach (var kvp in result)
			{
				Assert.That(kvp.Value, Does.Not.Contain(200),
					$"Path '{kvp.Key}' was included in results but contained a 200 status code.");
			}
		}

		[Test]
		public void Task1_NullSpec_ThrowsException()
		{
			// Assert exception handling for edge cases
			Assert.Throws<Exception>(() => Task1.UrlPathsWithResponseCodesNot200(null));
		}
	}

	[TestFixture]
	public class Task2Tests
	{
		private HttpbinApiService _apiService;

		[SetUp]
		public void Setup()
		{
			_apiService = new HttpbinApiService();
		}

		[Test]
		public async Task PostOrder_ReturnsSuccessfulResponseWithFormDataAndHeaders()
		{
			string expectedName = "Bassel Yasser";
			string expectedEmail = "basselyasser@example.com";

			var response = await _apiService.PostOrder(
				custName: expectedName,
				custTel: "123-456-7890",
				custEmail: expectedEmail,
				deliveryTime: "13:30",
				comments: "Beware the large bloodthirsty dog.",
                size: PizzaSize.medium,
                toppings: ["cheese", "mushroom"]
            );

			// Assert HTTP status response
			Assert.That(response, Is.Not.Null);
			Assert.That(response.Headers, Is.Not.Empty, "POST request should return response headers");

			// Process form & headers
			var (formDict, headersDict) = Task2.ResponseFormAndHeaders(response.Data, response.Headers);

			// Assert Form Data payload contents
			Assert.That(formDict, Is.Not.Null);
			Assert.That(formDict.ContainsKey("custname"), Is.True);
			Assert.That(formDict["custname"]?.ToString(), Is.EqualTo(expectedName));
			Assert.That(formDict["custemail"]?.ToString(), Is.EqualTo(expectedEmail));

			// Assert Header contents
			Assert.That(headersDict, Is.Not.Null.And.Not.Empty);
		}
	}

	[TestFixture]
	public class Task3Tests
	{
		private CountriesApiService _apiService;

		[SetUp]
		public void Setup()
		{
			_apiService = new CountriesApiService();
		}

		[Test]
		public async Task GetCountriesAsync_ReturnsCountryData()
		{
			var countries = await _apiService.GetCountriesAsync();

			Assert.That(countries, Is.Not.Null.And.Not.Empty);
		}

		[Test]
		public async Task Task3_GetAllUniqueLanguageCodes_ReturnsUniqueList()
		{
			var countries = await _apiService.GetCountriesAsync();
			Assert.That(countries, Is.Not.Null);

			var languageCodes = Task3.GetAllUniqueLanguageCodes(countries);

			Assert.That(languageCodes, Is.Not.Null.And.Not.Empty);
			Assert.That(languageCodes, Is.Unique, "Language code collection should contain unique values");
		}

		[TestCase("en")]
		[TestCase("es")]
		public async Task Task3_PopulationCountByLanguageCode_CalculatesPopulation(string langCode)
		{
			var result = await Task3.PopulationCountByLanguageCode(_apiService, new List<string> { langCode });

			Assert.That(result, Is.Not.Null);
			Assert.That(result.ContainsKey(langCode), Is.True);
			Assert.That(result[langCode], Is.GreaterThan(0), $"Population for '{langCode}' should be greater than zero");
		}
	}
}