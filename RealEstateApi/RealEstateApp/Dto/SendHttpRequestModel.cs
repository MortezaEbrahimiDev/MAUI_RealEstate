using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Dto
{
    public class SendHttpRequestModel
    {
        public HttpMethod HttpMethod { get; set; }
        public string  FinalUrlAddress{ get; set; }
        public AuthenticationHeaderValue authenticationHeaderValue { get; set; }
        public List<(string HeaderName, string HeaderVale)> Headers { get; set; } = new List<(string, string)>();
        public HttpContent Body { get; set; }
    }
}
