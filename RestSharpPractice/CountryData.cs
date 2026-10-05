using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RestSharpPractice
{
    internal class CountryData
    {
        [JsonPropertyName("languages")]
        public List<Dictionary<string, string>>? Languages { get; set; }

        [JsonPropertyName("population")]
        public long Population { get; set; }
    }
}
