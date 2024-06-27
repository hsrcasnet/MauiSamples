using Microsoft.Extensions.Logging;
using MonitoringDemo.Services;
using MonitoringDemo.ViewModels;
using MonitoringDemo.Views;
using NLog.Extensions.Logging;
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

            builder.Services.AddLogging(o =>
            {
                o.SetMinimumLevel(LogLevel.Trace);
                o.ClearProviders();
                o.AddNLog(NLogLoggerConfiguration.GetLoggingConfiguration());
                o.AddSentry(o =>
                {
                    // Sentry is initialized in SentryAnalytics
                    o.InitializeSdk = false;

                    // SentryLoggingOptions
                    o.MinimumBreadcrumbLevel = LogLevel.Debug;
                    o.MinimumEventLevel = LogLevel.Warning;

                    o.AddLogEntryFilter(new LogEntryFilter());
                });
            });

            builder.Services.AddSingleton<ISentryAnalytics, SentryAnalytics>();
            builder.Services.AddSingleton<IWorldTimeService, WorldTimeService>();
            builder.Services.AddSingleton<IPreferences>(_ => Preferences.Default);
            builder.Services.AddSingleton<IAppInfo>(_ => AppInfo.Current);
            builder.Services.AddSingleton<ILogFileReader>(_ => new NLogFileReader(NLogLoggerConfiguration.LogFilePath));
            builder.Services.AddSingleton<IEnvironmentSelector, EnvironmentSelector>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();

            return builder.Build();
        }
    }
}
