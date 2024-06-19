using Microsoft.Extensions.Logging;

namespace MonitoringDemo.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly ILogger logger;
        private readonly HttpClient httpClient;

        public WeatherForecastService(
            ILogger<WeatherForecastService> logger)
        {
            this.logger = logger;

            this.httpClient = new HttpClient(new SentryHttpMessageHandler());
        }

        public async Task<string> GetAsync()
        {
            this.logger.LogDebug("GetAsync");

            try
            {
                var result = await this.httpClient.GetStringAsync("https://www.google.ch");
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "GetAsync failed with exception");
                throw;
            }
        }
    }
}
