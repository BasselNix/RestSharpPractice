using NUnit.Framework;

namespace RestSharpPractice.Tests
{
	[TestFixture]
	public class Task2Tests
	{
		public required HttpbinApiService _apiService;

		[SetUp]
		public void Setup()
		{
			_apiService = new HttpbinApiService();
		}

		[Test]
        public async Task PostOrder_ReturnsSuccessfulResponseWithFormDataAndHeaders()
        {
            string expectedName = "Bassel Yasser";
            string expectedTel = "123-456-7890";
            string expectedEmail = "basselyasser@example.com";
            string expectedDeliveryTime = "13:30";
            string expectedComments = "Beware the large bloodthirsty dog.";
            var expectedSize = HttpbinApiService.PizzaSize.Medium;
            var expectedToppings = new List<string> { "cheese", "mushroom" };

            var response = await _apiService.PostOrder(
                custName: expectedName,
                custTel: expectedTel,
                custEmail: expectedEmail,
                deliveryTime: expectedDeliveryTime,
                comments: expectedComments,
                size: expectedSize,
                toppings: expectedToppings
            );

            Assert.That(response, Is.Not.Null);
            Assert.That(response.Headers, Is.Not.Empty, "POST request should return response headers");

            var (formDict, headersDict) = Task2.ResponseFormAndHeaders(response.Data, response.Headers);

            // Extract the topping value from the form dictionary
            object? toppingValue = formDict.ContainsKey("topping") ? formDict["topping"] : null;

            List<string> actualToppings = new();

            if (toppingValue is System.Text.Json.JsonElement jsonElement)
                actualToppings = jsonElement.EnumerateArray()
                    .Select(e => e.GetString() ?? string.Empty)
                    .ToList();

            using (Assert.EnterMultipleScope())
            {
                // Assert Form Data container
                Assert.That(formDict, Is.Not.Null, "Form dictionary should not be null.");

                // Assert individual fields
                Assert.That(formDict, Contains.Key("custname"));
                Assert.That(formDict?["custname"]?.ToString(), Is.EqualTo(expectedName));

                Assert.That(formDict, Contains.Key("custtel"));
                Assert.That(formDict?["custtel"]?.ToString(), Is.EqualTo(expectedTel));

                Assert.That(formDict, Contains.Key("custemail"));
                Assert.That(formDict?["custemail"]?.ToString(), Is.EqualTo(expectedEmail));

                Assert.That(formDict, Contains.Key("delivery"));
                Assert.That(formDict?["delivery"]?.ToString(), Is.EqualTo(expectedDeliveryTime));

                Assert.That(formDict, Contains.Key("comments"));
                Assert.That(formDict?["comments"]?.ToString(), Is.EqualTo(expectedComments));

                Assert.That(formDict, Contains.Key("size"));
                Assert.That(formDict?["size"]?.ToString(), Is.EqualTo(expectedSize.ToString().ToLower()));

                Assert.That(formDict, Contains.Key("topping"));
                Assert.That(actualToppings, Is.EquivalentTo(expectedToppings));

                Assert.That(headersDict, Is.Not.Null.And.Not.Empty, "Headers dictionary should not be empty.");
            };
        }
	}
}