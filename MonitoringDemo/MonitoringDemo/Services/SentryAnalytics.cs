namespace MonitoringDemo.Services
{
    public class SentryAnalytics : ISentryAnalytics
    {
        public SentryAnalytics()
        {
        }

        public void CaptureMessage(string message, SentryLevel sentryLevel = SentryLevel.Info)
        {
            SentrySdk.CaptureMessage(message, sentryLevel);
        }

        public void AddBreadcrumb(string message, IDictionary<string, string> data = null)
        {
            SentrySdk.AddBreadcrumb(message, data: data);
        }

        public void CaptureException(Exception exception)
        {
            SentrySdk.CaptureException(exception);
        }
    }
}
