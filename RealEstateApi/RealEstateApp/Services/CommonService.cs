using Newtonsoft.Json;
using RealEstateApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Services
{
    public static class CommonService
    {
        public static async Task<HttpResponseMessage> SendRequest(SendHttpRequestModel requestModel)
        {

            using (var httpClient = new HttpClient(GetHandler()))
            {
                HttpRequestMessage httpRequest = new();
                httpRequest.Method = requestModel.HttpMethod;
                httpRequest.RequestUri = new Uri(requestModel.FinalUrlAddress);
                httpRequest.Content = requestModel.Body;

                if (requestModel.authenticationHeaderValue != null)
                {
                    httpRequest.Headers.Authorization = requestModel.authenticationHeaderValue;
                }

                if (requestModel.Headers.Any())
                {
                    foreach (var header in requestModel.Headers)
                    {
                        httpRequest.Headers.Add(header.HeaderName, header.HeaderVale);
                    }
                }

                try
                {
                    var httpResponseMessage = await httpClient.SendAsync(httpRequest);

                    return httpResponseMessage;
                }
                catch (HttpRequestException e)
                {
                    throw new Exception();
                }

            }
        }


        private static HttpClientHandler GetHandler()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            return handler;
        }
    }
}
