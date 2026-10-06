using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace RestSharpPractice
{
    internal class Task2
    {
        public static Tuple<Dictionary<string, object>, Dictionary<string, object>> ResponseFormAndHeaders(RestResponse response)
        {
            // Get the "form" element in response
            JsonDocument doc = JsonDocument.Parse(response.Content);
            JsonElement root = doc.RootElement;
            root.TryGetProperty("form", out JsonElement formElement);
            var formDict = JsonSerializer.Deserialize<Dictionary<string, object>>(formElement.GetRawText());

            // Get the response headers
            var headersDict = response.Headers.ToDictionary(h => h.Name, h => (object)h.Value);

            var taskSolution = Tuple.Create(formDict, headersDict);
            return taskSolution;
        }
    }
}
