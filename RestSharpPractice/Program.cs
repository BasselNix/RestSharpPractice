using RestSharp;
using RestSharpPractice;
using System.Text.Json;

short taskNumber = 0;
while (taskNumber < 1 || taskNumber > 3)
{
    Console.Write("Enter task number (1, 2, or 3): ");
    short.TryParse(Console.ReadLine(), out taskNumber);
}

switch(taskNumber)
{
    case 1:
    {
        try
        {
            var options = new RestClientOptions("https://nghttp2.org/httpbin/")
            {
                Timeout = TimeSpan.FromSeconds(45)
            };

            var client = new RestClient(options);
            client.AddDefaultHeader("User-Agent", "Learning Automation");

            var request = new RestRequest("spec.json");
            var response = await client.GetAsync<SpecData>(request);
            if (response == null || response.Paths.Count == 0)
                throw new Exception("Request failed to get spec.json from the URL");

            var solutionDict = Task1.UrlPathsWithResponseCodesNot200(response);

            string prettyJson = JsonSerializer.Serialize(solutionDict, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(prettyJson);
        }
        catch (TimeoutException)
        {
            Console.WriteLine("The request timed out while contacting the gateway.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        break;
    }
    case 2:
    {
        try
        {
            var options = new RestClientOptions("https://httpbin.org/post")
            {
                Timeout = TimeSpan.FromSeconds(45)
            };

            var client = new RestClient(options);
            client.AddDefaultHeader("User-Agent", "Learning Automation");

            var request = new RestRequest("", Method.Post);
            request.AddParameter("custname", "Bassel Yasser");
            request.AddParameter("custtel", "123-456-7890");
            request.AddParameter("custemail", "basselyasser@example.com");
            request.AddParameter("delivery", "13:30");
            request.AddParameter("comments", "Beware the large bloodthirsty dog.");

            var response = client.Execute(request);
            if (response.IsSuccessful == false || response == null)
                throw new Exception("POST request returned null response");

            var solutionTuple = Task2.ResponseFormAndHeaders(response);

            string prettyJson = JsonSerializer.Serialize(solutionTuple, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine("\"Item1\" is the Form part of the response json, and \"Item2\" are the headers.");
            Console.WriteLine(prettyJson);
        }
        catch (TimeoutException)
        {
            Console.WriteLine("The request timed out while contacting the gateway.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        break;
    }
    case 3:
    {
        try
        {
            var options = new RestClientOptions("https://countries.dev/")
            {
                Timeout = TimeSpan.FromSeconds(45)
            };

            var client = new RestClient(options);
            client.AddDefaultHeader("User-Agent", "Learning Automation");

            var countriesRequest = new RestRequest("/countries/");
            var response = await client.GetAsync<List<CountryData>>(countriesRequest);

            if (response == null)
                throw new Exception("Request failed to get countries data");

            var languageCodes = Task3.GetAllUniqueLanguageCodes(response);

            string prettyJson = JsonSerializer.Serialize(languageCodes, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(prettyJson);

            Console.WriteLine($"Fetching population data...");

            var populationCountByLanguageCode = await Task3.PopulationCountByLanguageCode(client, languageCodes);

            prettyJson = JsonSerializer.Serialize(populationCountByLanguageCode, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(prettyJson);
        }
        catch (TimeoutException)
        {
            Console.WriteLine("The request timed out while contacting the gateway.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        break;
    }
    default:
        break;
}