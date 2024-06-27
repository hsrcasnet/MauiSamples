namespace MonitoringDemo.Services
{
    public class EnvironmentSelector : IEnvironmentSelector
    {
        public AppEnvironment GetCurrentEnvironment()
        {
            return new AppEnvironment
            {
                Name = "test",
                SentryDsn = "https://110d89aff7d08b72cc624cbc86d3b6f1@o4507458300280832.ingest.de.sentry.io/4507458302509136",
            };
        }
    }
}