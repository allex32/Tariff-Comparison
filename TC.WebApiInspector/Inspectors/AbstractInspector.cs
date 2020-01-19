using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TC.WebApiInspector.Client;
using TC.WebApiInspector.Extensions;

namespace TC.WebApiInspector.Inspectors
{
    public abstract class AbstractInspector
    {
        protected ILogger _logger;
        public AbstractInspector(ILogger logger)
        {
            this._logger = logger;
        }
        /// <summary>
        /// Generalized method for invocation of api endpoint's methods.
        /// It contains basic error handling logic.
        /// </summary>
        /// <param name="methodName">Name of the called method (used in generation of result string)</param>
        /// <param name="resultMessage">StringBuilder containing result message</param>
        /// <param name="runMethod">Body of the called method</param>
        /// <returns>bool to indicate if the called method is completed with success status code</returns>
        protected async Task<bool> RunApiMethod<T>(string methodName, StringBuilder resultMessage, Func<Task<SwaggerResponse<T>>> runMethod)
        {
            try
            {
                var result = await runMethod();
                if (result.IsSuccessStatusCode())
                {
                    resultMessage.AppendLine($"{methodName} : success status code - {result.StatusCode}");
                }
                else
                {
                    resultMessage.AppendLine($"{methodName} : error status code - {result.StatusCode}");
                }

                return result.IsSuccessStatusCode();
            }
            catch (TariffApiException ex)
            {
                resultMessage.AppendLine($"{methodName} : an exception is thrown. Http Status code {ex.StatusCode}");

                return false;
            }
        }
    }
}
