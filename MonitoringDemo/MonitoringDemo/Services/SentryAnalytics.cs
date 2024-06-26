namespace MonitoringDemo.Services
{
    public class SentryAnalytics : ISentryAnalytics
    {
        public SentryAnalytics()
        {
        }

        public void CaptureMessage(string message, SentryLevel sentryLevel = SentryLevel.Info, IDictionary<string, string> data = null)
        {
            SentrySdk.CaptureMessage(message, s =>
            {
                if (data != null)
                {
                    s.Contexts["data"] = data;
                }
            }, sentryLevel);
        }

        public void AddBreadcrumb(string message, IDictionary<string, string> data = null)
        {
            SentrySdk.AddBreadcrumb(message, data: data);
        }

        public void CaptureException(Exception exception)
        {
            SentrySdk.CaptureException(exception, s => s.AddAttachment(new byte[] { 0x54, 0x65, 0x73, 0x74, 0x20 }, "filename.log", AttachmentType.Default));
        }
    }
}
