using Microsoft.Extensions.Configuration;
using SAP.Middleware.Connector;

namespace SapConnectionTest.Configurations
{
    public class SapDestination : IDestinationConfiguration
    {
        private readonly IConfiguration _configuration;

        public SapDestination(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;

        public bool ChangeEventsSupported()
        {
            return false;
        }

        public RfcConfigParameters GetParameters(string destinationName)
        {
            var rfcConfiguration = _configuration.GetSection(nameof(RfcConfiguration)).Get<RfcConfiguration>();

            RfcConfigParameters parameters = new()
            {
                { RfcConfigParameters.Name, rfcConfiguration.Name },
                { RfcConfigParameters.AppServerHost, rfcConfiguration.AppServerHost },
                { RfcConfigParameters.SystemNumber, rfcConfiguration.SystemNumber },
                { RfcConfigParameters.SystemID, rfcConfiguration.SystemID },
                { RfcConfigParameters.User, rfcConfiguration.User },
                { RfcConfigParameters.Password, rfcConfiguration.Password },
                { RfcConfigParameters.Client, rfcConfiguration.Client },
                { RfcConfigParameters.Language, rfcConfiguration.Language },
                { RfcConfigParameters.PoolSize, rfcConfiguration.PoolSize }
            };
            return parameters;
        }
    }
}
