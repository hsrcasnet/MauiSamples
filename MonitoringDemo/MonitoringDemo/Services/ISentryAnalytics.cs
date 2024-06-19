
namespace MonitoringDemo.Services
{
    public interface ISentryAnalytics
    {
        void AddBreadcrumb(string message, IDictionary<string, string> data = null);

        void CaptureException(Exception exception);

        void CaptureMessage(string message, SentryLevel sentryLevel = SentryLevel.Info);
    }
}