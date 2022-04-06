using Microsoft.Extensions.Configuration;
using SAP.Middleware.Connector;
using SapConnectionTest.Configurations;
using System;

namespace SapConnectionTest.Services
{
    public class PingTest
    {
        private readonly IConfiguration _configuration;
        private RfcDestination _rfcDestination;

        public PingTest(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        internal void Ping()
        {
            var rfcConfiguration = _configuration.GetSection(nameof(RfcConfiguration)).Get<RfcConfiguration>();

            try
            {
                _rfcDestination = RfcDestinationManager.GetDestination(rfcConfiguration.Name);

                _rfcDestination.Ping();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
