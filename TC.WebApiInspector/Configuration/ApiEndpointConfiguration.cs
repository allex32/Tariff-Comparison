using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TC.WebApiInspector.Configuration
{
    [JsonObject("ApiEndpoint")]
    public class ApiEndpointConfiguration
    {
        [JsonProperty("BaseUrl")]
        public string BaseUrl { get; set; }
    }
}
