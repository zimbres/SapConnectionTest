using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SapConnectionTest.Configurations;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SapConnectionTest.Services
{
    public class ImAliveService
    {
        private readonly ILogger<ImAliveService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;

        public ImAliveService(ILogger<ImAliveService> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _client = _httpClientFactory.CreateClient("Defalt");
        }

        internal async Task Push(string param = null)
        {
            var applicationSettings = _configuration.GetSection(nameof(ApplicationSettings)).Get<ApplicationSettings>();

            try
            {
                await _client.GetAsync(applicationSettings.PushUrl + param);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.Message);
            }
        }
    }
}
