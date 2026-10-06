using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RestSharpPractice
{
    public class SpecData
    {
        [JsonPropertyName("paths")]
        public Dictionary<string, PathItem> Paths { get; set; }
    }

    public class PathItem : Dictionary<string, Operation>
    {
        // Maps HTTP methods (get, post, put, delete, etc.) to their respective operations
    }

    public class Operation
    {
        [JsonPropertyName("responses")]
        public Dictionary<string, ResponseDetails> Responses { get; set; }
    }

    public class ResponseDetails
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
