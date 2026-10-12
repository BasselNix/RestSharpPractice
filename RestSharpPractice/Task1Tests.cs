using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;

namespace RestSharpPractice.Tests
{
	[TestFixture]
	public class Task1Tests
	{
		public required Nghttp2ApiService _apiService;

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
}