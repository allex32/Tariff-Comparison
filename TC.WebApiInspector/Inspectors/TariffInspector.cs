
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TC.WebApiInspector.Client;
using TC.WebApiInspector.Configuration;
using TC.WebApiInspector.Extensions;

namespace TC.WebApiInspector.Inspectors
{
    public class TariffInspector : AbstractInspector, IInspector
    { 
        private readonly TariffClient _tariffClient;
        
        public TariffInspector(IOptionsMonitor<ApiEndpointConfiguration> configuration, ILogger<TariffInspector> logger)
            : base(logger)
        {
            _tariffClient = new TariffClient(configuration.CurrentValue.BaseUrl);
        }

        public async Task Inspect()
        {
            await this.InspectTariffCompareMethod();
        }

        private async Task InspectTariffCompareMethod()
        {
            SwaggerResponse<ICollection<GetCalculatedTariffModel>> swaggerResponse = null;
            StringBuilder resultMessage = new StringBuilder();
            string currentMethodName = nameof(InspectTariffCompareMethod);

            decimal testConsumptionValue = 100;

            var isSuccess = await RunApiMethod(currentMethodName, resultMessage, async () =>
            {
                swaggerResponse = await _tariffClient.CompareAsync(testConsumptionValue);
                return swaggerResponse;
            });

            if (isSuccess)
            {
                var isAssertionConfirmed = true;
                var tariffs = swaggerResponse.Result.ToArray();
                isAssertionConfirmed &= string.Equals("Basic electricity tariff", tariffs[0].TariffName);
                isAssertionConfirmed &= tariffs[0].AnnualCosts == 82;
                isAssertionConfirmed &= tariffs[0].AnnualConsumption == 100;
              
                if (isAssertionConfirmed)
                {
                    resultMessage.AppendLine($"{currentMethodName} : Passed");
                }
                else
                {
                    resultMessage.AppendLine($"{currentMethodName} : Failed");
                }
            }

            _logger.Log(LogLevel.Information, resultMessage.ToString());
        }
    }
}
