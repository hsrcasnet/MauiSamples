using Microsoft.Extensions.Logging;

namespace MonitoringDemo.Services
{
    public class WorldTimeService : IWorldTimeService
    {
        private readonly ILogger logger;
        private readonly HttpClient httpClient;

        public WorldTimeService(
            ILogger<WorldTimeService> logger)
        {
            this.logger = logger;

            this.httpClient = new HttpClient(new SentryHttpMessageHandler());
        }

        public async Task<string> GetTimeZoneInfoAsync(string timeZone)
        {
            this.logger.LogDebug("GetTimeZoneInfoAsync");

            try
            {
                var url = $"https://worldtimeapi.org/api/timezone/{timeZone}";
                var result = await this.httpClient.GetStringAsync(url);
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "GetTimeZoneInfoAsync failed with exception");
                throw;
            }
        }
    }
}
