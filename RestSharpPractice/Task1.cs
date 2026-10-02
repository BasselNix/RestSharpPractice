using RestSharp;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;

namespace RestSharpPractice
{
    internal class Task1
    {
        public static Dictionary<string, string> urlPathsWithResponseCodesNot200(Dictionary<string, object> responseJson)
        {
            var taskSolution = new Dictionary<string, string>();

            /* Example JSON:
              {
                "paths": {
                    "/absolute-redirect/{n}": {
                        "get": {
                            "parameters": [
                                {
                                    "in": "path",
                                    "name": "n",
                                    "type": "int"
                                }
                            ],
                            "produces": [
                                "text/html"
                            ],
                            "responses": {
                                "302": {
                                    "description": "A redirection."
                                }
                            }
                        }
                    }
                }
            */
            // 1. Navigate to the "paths" object
            if (responseJson["paths"] is JsonElement pathsElement)
            {
                var pathsMap = JsonSerializer.Deserialize<Dictionary<string, object>>(pathsElement.GetRawText());

                if (pathsMap != null)
                {
                    // 2. Loop through each individual path (e.g., "/absolute-redirect/{n}")
                    foreach (var pathKey in pathsMap.Keys)
                    {
                        var methodsElement = (JsonElement)pathsMap[pathKey];
                        var methodsMap = JsonSerializer.Deserialize<Dictionary<string, object>>(methodsElement.GetRawText());

                        if (methodsMap == null)
                            continue;

                        // 3. Loop through HTTP methods (get, post, patch, etc.)
                        foreach (var methodKey in methodsMap.Keys)
                        {
                            var detailsElement = (JsonElement)methodsMap[methodKey];
                            var detailsMap = JsonSerializer.Deserialize<Dictionary<string, object>>(detailsElement.GetRawText());

                            // 4. Look for the "responses" block
                            if (detailsMap != null && detailsMap.TryGetValue("responses", out var responsesObj) && responsesObj is JsonElement responsesElement)
                            {
                                var responsesMap = JsonSerializer.Deserialize<Dictionary<string, object>>(responsesElement.GetRawText());

                                if (responsesMap == null)
                                    continue;

                                // 5. Check each status code key inside "responses"
                                foreach (var statusCode in responsesMap.Keys)
                                {
                                    if (statusCode != "200")
                                    {
                                        // Store the path and the non-200 status code
                                        // Stores only one non-200 status code per path
                                        taskSolution[pathKey] = statusCode;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                throw new Exception("Response does not contain paths key");
            }

            return taskSolution;
        }
    }
}
