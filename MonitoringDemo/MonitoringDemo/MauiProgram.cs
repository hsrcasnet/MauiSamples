using Microsoft.Extensions.Logging;
using MonitoringDemo.Services;
using MonitoringDemo.ViewModels;
using MonitoringDemo.Views;
using Sentry.Extensions.Logging;

namespace MonitoringDemo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                //.UseSentry(o =>
                //{
                //    // Configure SentryOptions here
                //    // if Microsoft.Extensions.Logging integration is not used
                //    o.Dsn = Constants.SentryDsn;
                //})
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
            builder.Logging.AddSentry(o =>
            {
                // SentryOptions
                o.Dsn = Constants.SentryDsn;

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
                o.Environment = "test";
                o.SampleRate = 1.0f;
                o.AttachStacktrace = true;
                o.CaptureFailedRequests = true;

                // SentryLoggingOptions
                o.MinimumBreadcrumbLevel = LogLevel.Debug;
                o.MinimumEventLevel = LogLevel.Warning;

                // Filter exceptions by type or using a custom logic
                o.AddExceptionFilterForType<TaskCanceledException>();
                o.AddExceptionFilter(new SentryExceptionFilter());
                o.AddLogEntryFilter(new LogEntryFilter());

                // Modifications to event before it's sent to sentry.
                o.SetBeforeSend((e, h) =>
                {
                    if (e.Level >= SentryLevel.Warning)
                    {
                        var content = new ByteAttachmentContent(new byte[] { 0x54, 0x65, 0x73, 0x74, 0x20 });
                        var attachment = new SentryAttachment(AttachmentType.Default, content, "logfile.log", "text/plain");
                        h.Attachments.Add(attachment);
                    }

                    // In order to drop events, just return null.

                    return e;
                });
            });
#endif

            builder.Services.AddSingleton<ISentryAnalytics, SentryAnalytics>();
            builder.Services.AddSingleton<IWorldTimeService, WorldTimeService>();
            builder.Services.AddSingleton<IPreferences>(_ => Preferences.Default);

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();

            return builder.Build();
        }
    }
}
