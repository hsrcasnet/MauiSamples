
namespace MonitoringDemo.Services
{
    public interface IWeatherForecastService
    {
        Task<string> GetAsync();
    }
}