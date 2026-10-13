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
            var expectedForm = new PizzaOrderForm
            {
                CustName = "Bassel Yasser",
                CustTel = "123-456-7890",
                CustEmail = "basselyasser@example.com",
                Delivery = "13:30",
                Comments = "Beware the large bloodthirsty dog.",
                Size = HttpbinApiService.PizzaSize.Medium.ToString().ToLower(),
                Topping = new List<string> { "cheese", "mushroom" }
            };

            var response = await _apiService.PostOrder(
                custName: expectedForm.CustName,
                custTel: expectedForm.CustTel,
                custEmail: expectedForm.CustEmail,
                deliveryTime: expectedForm.Delivery,
                comments: expectedForm.Comments,
                size: HttpbinApiService.PizzaSize.Medium,
                toppings: expectedForm.Topping
            );

            Assert.That(response, Is.Not.Null);
            Assert.That(response.Headers, Is.Not.Empty, "POST request should return response headers");

            var (actualForm, headersDict) = Task2.ResponseFormAndHeaders(response.Data, response.Headers);

            using (Assert.EnterMultipleScope())
            {
                Assert.That(actualForm, Is.EqualTo(expectedForm).UsingPropertiesComparer(),
                    "The submitted form data should match the response form data.");

                Assert.That(response.Headers, Is.Not.Null.And.Not.Empty,
                    "Headers dictionary should not be empty.");
            }
        }
	}
}