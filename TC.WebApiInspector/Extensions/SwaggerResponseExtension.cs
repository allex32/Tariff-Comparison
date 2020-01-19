using System;
using System.Collections.Generic;
using System.Text;
using TC.WebApiInspector.Client;

namespace TC.WebApiInspector.Extensions
{
    public static class SwaggerResponseExtension
    {
        public static bool IsSuccessStatusCode(this SwaggerResponse swaggerResponse)
        {
            return swaggerResponse.StatusCode >= 200 
                && swaggerResponse.StatusCode <= 299;
        }
    }
}
