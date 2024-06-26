
namespace MonitoringDemo.Services
{
    /// <summary>
    /// Interface which abstracts sentry user tracking and error reporting features.
    /// This interface has the same purpose as <see cref="ISentryClient"/> but comes with the advantage
    /// that it's owned by us.
    /// </summary>
    public interface ISentryAnalytics
    {
        void CaptureMessage(string message, SentryLevel sentryLevel = SentryLevel.Info, IDictionary<string, string> data = null);

        void AddBreadcrumb(string message, IDictionary<string, string> data = null);

        void CaptureException(Exception exception);
    }
}