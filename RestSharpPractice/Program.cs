using RestSharpPractice;
using System.Text.Json;
using static RestSharpPractice.HttpbinApiService;

short taskNumber = 0;
while (taskNumber < 1 || taskNumber > 3)
{
    Console.Write("Enter task number (1, 2, or 3): ");
    short.TryParse(Console.ReadLine(), out taskNumber);
}

try
{
    switch(taskNumber)
    {
        case 1:
        {
            var nghttpService = new Nghttp2ApiService();
            var response = await nghttpService.GetSpecJsonAsync();
            if (response == null || response.Paths.Count == 0)
                throw new Exception("Request failed to get spec.json from the URL");

            var solutionDict = Task1.UrlPathsWithResponseCodesNot200(response);

            string prettyJson = JsonSerializer.Serialize(solutionDict, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(prettyJson);

            break;
        }
        case 2:
        {
            var httpbinPostService = new HttpbinApiService();
            var (data, headers) = await httpbinPostService.PostOrder(
                custName: "Bassel Yasser",
                custTel: "123-456-7890",
                custEmail: "basselyasser@example.com",
                deliveryTime: "13:30",
                comments: "Beware the large bloodthirsty dog.",
                size: PizzaSize.Medium,
                toppings: ["cheese", "mushroom"]
            );

            var solutionTuple = Task2.ResponseFormAndHeaders(data, headers);

            string prettyJson = JsonSerializer.Serialize(solutionTuple, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine("\"Item1\" is the Form part of the response json, and \"Item2\" are the headers.");
            Console.WriteLine(prettyJson);

            break;
        }
        case 3:
        {
            var countriesService = new CountriesApiService();
            var response = await countriesService.GetCountriesAsync() ?? throw new Exception("Request failed to get countries data");

            var languageCodes = Task3.GetAllUniqueLanguageCodes(response);

            string prettyJson = JsonSerializer.Serialize(languageCodes, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(prettyJson);

            Console.WriteLine($"Fetching population data...");

            var populationCountByLanguageCode = await Task3.PopulationCountByLanguageCode(countriesService, languageCodes);

            prettyJson = JsonSerializer.Serialize(populationCountByLanguageCode, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(prettyJson);

            break;
        }
        default:
            break;
    }
}
catch (TimeoutException)
{
    Console.WriteLine("The request timed out while contacting the gateway.");
}
catch (Exception e)
{
    Console.WriteLine(e.ToString());
}