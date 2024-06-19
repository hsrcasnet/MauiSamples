using MonitoringDemo.Services;
using MonitoringDemo.ViewModels;
using MonitoringDemo.Views;
using Microsoft.Extensions.Logging;

namespace MonitoringDemo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<IAnalytics, AppCenterAnalytics>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();

            return builder.Build();
        }
    }
}
