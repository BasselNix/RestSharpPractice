using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestSharpPractice
{
    internal class HttpbinApiService
    {
        private readonly RestClient _client;

        public HttpbinApiService(string baseUrl = "https://httpbin.org/")
        {
            var options = new RestClientOptions(baseUrl)
            {
                Timeout = TimeSpan.FromSeconds(35)
            };

            _client = new RestClient(options);
            _client.AddDefaultHeader("User-Agent", "Learning Automation");
        }

        // Sends a POST request to the /post endpoint with form parameters.
        public RestResponse PostOrder(
            string custName,
            string custTel,
            string custEmail,
            string deliveryTime,
            string comments)
        {
            var request = new RestRequest("post", Method.Post);

            request.AddParameter("custname", custName);
            request.AddParameter("custtel", custTel);
            request.AddParameter("custemail", custEmail);
            request.AddParameter("delivery", deliveryTime);
            request.AddParameter("comments", comments);

            return _client.Execute(request);
        }
    }
}
