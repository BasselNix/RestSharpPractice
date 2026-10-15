using NUnit.Framework;

namespace RestSharpPractice.Tests
{
	[TestFixture]
	public class Task3Tests
	{
		public required CountriesApiService _apiService;

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