namespace MonitoringDemo.Services
{
    public interface IEnvironmentSelector
    {
        AppEnvironment GetCurrentEnvironment();
    }
}