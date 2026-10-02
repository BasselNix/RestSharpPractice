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
            var client = new RestClient("https://nghttp2.org/httpbin/");
            client.AddDefaultHeader("User-Agent", "Learning Automation");

            var request = new RestRequest("spec.json");
            var response = await client.GetAsync<Dictionary<string, object>>(request);
            if (response == null || response.Count == 0)
                throw new Exception("Request failed to get spec.json from the URL");

            var solutionDict = Task1.UrlPathsWithResponseCodesNot200(response);

            string prettyJson = JsonSerializer.Serialize(solutionDict, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(prettyJson);
        }
        catch(Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        break;
    }
    case 2:
    {
        break;
    }
    case 3:
    {
        break;
    }
    default:
        break;
}