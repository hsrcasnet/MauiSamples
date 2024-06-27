using System.Net.Mime;

namespace MonitoringDemo.Services
{
    public class SentryAnalytics : ISentryAnalytics, IDisposable
    {
        private readonly ILogFileReader logFileReader;
        private readonly IAppInfo appInfo;
        private readonly IEnvironmentSelector environmentSelector;
        private readonly IDisposable disposable;

        public SentryAnalytics(
            ILogFileReader logFileReader,
            IAppInfo appInfo,
            IEnvironmentSelector environmentSelector)
        {
            this.logFileReader = logFileReader;
            this.appInfo = appInfo;
            this.environmentSelector = environmentSelector;

            this.disposable = SentrySdk.Init(this.ConfigureSentry);
        }

        private void ConfigureSentry(SentryOptions o)
        {
            this.disposable?.Dispose();

            var appEnvironment = this.environmentSelector.GetCurrentEnvironment();

            // SentryOptions
            o.Dsn = appEnvironment.SentryDsn;
            o.Environment = appEnvironment.Name;
            o.Release = $"{this.appInfo.VersionString} ({this.appInfo.BuildString})";

            // Use debug mode if you want to see what the SDK is doing.
            // Debug messages are written to stdout with Console.Writeline,
            // and are viewable in your IDE's debug console or with 'adb logcat', etc.
            // This option is not recommended when deploying your application.
#if DEBUG
            o.Debug = true;
#endif

            // Set TracesSampleRate to 1.0 to capture 100% of transactions for performance monitoring.
            // We recommend adjusting this value in production.
            o.TracesSampleRate = 1.0f;

            o.AutoSessionTracking = true;
            o.IsGlobalModeEnabled = true;
            o.SampleRate = 1.0f;
            o.AttachStacktrace = true;
            o.CaptureFailedRequests = true;
            o.MaxBreadcrumbs = 100;

            // Filter exceptions by type or using custom logic
            o.AddExceptionFilterForType<TaskCanceledException>();
            o.AddExceptionFilter(new SentryExceptionFilter());

            // Modifications to event before it is sent to sentry.
            o.SetBeforeSend((e, h) =>
            {
                if (e.Level >= SentryLevel.Warning)
                {
                    foreach (var sentryAttachment in this.GetSentryAttachmentFromLogFiles())
                    {
                        h.Attachments.Add(sentryAttachment);
                    }
                }

                // In order to drop events, just return null.

                return e;
            });
        }

        private IEnumerable<SentryAttachment> GetSentryAttachmentFromLogFiles()
        {
            // Before we can get any log files, we have to make sure
            // the logging subsystem has written all logs to the file.
            NLog.LogManager.Flush();

            var logFilePaths = this.logFileReader.EnumerateLogFiles();
            foreach (var logFilePath in logFilePaths)
            {
                yield return this.GetSentryAttachmentFromLogFile(logFilePath);
            }
        }

        private SentryAttachment GetSentryAttachmentFromLogFile(string logFilePath)
        {
            var attachmentContent = new FileAttachmentContent(logFilePath);
            var sentryAttachment = new SentryAttachment(AttachmentType.Default, attachmentContent, Path.GetFileName(logFilePath), MediaTypeNames.Text.Plain);
            return sentryAttachment;
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
            SentrySdk.CaptureException(exception, s =>
            {
                foreach (var sentryAttachment in this.GetSentryAttachmentFromLogFiles())
                {
                    s.AddAttachment(sentryAttachment);
                }
            });
        }

        public void Dispose()
        {
            this.disposable?.Dispose();
        }
    }
}
