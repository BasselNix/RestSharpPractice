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
        public static Dictionary<string, int[]> UrlPathsWithResponseCodesNot200(SpecData spec)
        {
            var taskSolution = new Dictionary<string, int[]>();

            if (spec?.Paths == null)
                throw new Exception("Response does not contain paths");

            foreach (var (path, pathItem) in spec.Paths)
            {
                if (pathItem == null)
                    continue;

                // Extract and parse all response status codes across all HTTP methods for this path
                var statusCodes = pathItem.Values
                    .Where(operation => operation?.Responses != null)
                    .SelectMany(operation => operation.Responses.Keys)
                    .Select(code => int.TryParse(code, out int parsedCode) ? parsedCode : (int?)null)
                    .Where(code => code.HasValue)
                    .Select(code => code.Value)
                    .Distinct()
                    .ToArray();

                // If 200 is not present in the status codes, add the path and its codes to the dictionary
                if (!statusCodes.Contains(200))
                {
                    taskSolution[path] = statusCodes;
                }
            }

            return taskSolution;
        }
    }
}
