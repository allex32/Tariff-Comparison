
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TC.WebApiInspector.Client;
using TC.WebApiInspector.Configuration;

namespace TC.WebApiInspector.Inspectors
{
    public class TariffInspector : IInspector
    { 
        private readonly TariffClient _tariffClient;
        private readonly ILogger<TariffInspector> _logger;

        public TariffInspector(IOptionsMonitor<ApiEndpointConfiguration> configuration, ILogger<TariffInspector> logger)
        {
            _logger = logger;
            _tariffClient = new TariffClient(configuration.CurrentValue.BaseUrl);
        }

        public void Inspect()
        {
            _logger.LogInformation("Started");
        }
    }
}
