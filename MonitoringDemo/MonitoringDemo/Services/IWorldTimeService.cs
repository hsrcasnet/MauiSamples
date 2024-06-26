
namespace MonitoringDemo.Services
{
    public interface IWorldTimeService
    {
        Task<string> GetTimeZoneInfoAsync(string timeZone);
    }
}