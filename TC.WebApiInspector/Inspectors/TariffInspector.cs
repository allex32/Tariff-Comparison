
using Microsoft.Extensions.Logging;
using TC.WebApiInspector.Client;
using TC.WebApiInspector.Configuration;

namespace TC.WebApiInspector.Inspectors
{
    public class TariffInspector : IInspector
    { 
        private readonly TariffClient tariffClient;
        private readonly ILogger<TariffInspector> logger;

        public TariffInspector(ApiEndpointConfiguration configuration, ILogger<TariffInspector> logger)
        {
            this.logger = logger;
            tariffClient = new TariffClient(configuration.BaseUrl);
        }

        public void Inspect()
        {
            logger.LogInformation("Started");
        }
    }
}
