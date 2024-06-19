using Microsoft.Extensions.Logging;
using MonitoringDemo.Services;
using MonitoringDemo.ViewModels;
using MonitoringDemo.Views;

namespace MonitoringDemo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSentry(options =>
                {
                    // The DSN is the only required setting.
                    options.Dsn = Constants.SentryDsn;

                    // Use debug mode if you want to see what the SDK is doing.
                    // Debug messages are written to stdout with Console.Writeline,
                    // and are viewable in your IDE's debug console or with 'adb logcat', etc.
                    // This option is not recommended when deploying your application.
                    options.Debug = true;

                    // Set TracesSampleRate to 1.0 to capture 100% of transactions for performance monitoring.
                    // We recommend adjusting this value in production.
                    options.TracesSampleRate = 1.0f;

                    options.AutoSessionTracking = true;
                    options.IsGlobalModeEnabled = true;
                    options.Environment = "test";
                    options.SampleRate = 1.0f;
                    options.AttachStacktrace = true;
                    options.CaptureFailedRequests = true;

                    // Other Sentry options can be set here.
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
            builder.Logging.AddSentry(o =>
            {
                o.Dsn = Constants.SentryDsn;
                o.Debug = true;
            });
#endif

            builder.Services.AddSingleton<IAppCenterAnalytics, AppCenterAnalytics>();
            builder.Services.AddSingleton<ISentryAnalytics, SentryAnalytics>();
            builder.Services.AddSingleton<IWeatherForecastService, WeatherForecastService>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();

            return builder.Build();
        }
    }
}
