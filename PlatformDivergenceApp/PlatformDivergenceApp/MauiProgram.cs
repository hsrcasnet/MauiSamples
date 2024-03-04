using InvokePlatformCodeDemos.Services.PartialMethods;
using Microsoft.Extensions.Logging;
using PlatformDivergenceApp.Platforms.Services;
using PlatformDivergenceApp.Services;
using PlatformDivergenceApp.Services.Settings;
using PlatformDivergenceApp.ViewModels;
using PlatformDivergenceApp.Views;

namespace PlatformDivergenceApp
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
            // Register views and viewmodels
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();

            builder.Services.AddTransient<SettingsPage>();
            builder.Services.AddTransient<SettingsViewModel>();

            builder.Services.AddTransient<DeviceOrientationPage>();
            builder.Services.AddTransient<DeviceOrientationViewModel>();

            // Register services
            builder.Services.AddSingleton<IDeviceOrientationService, DeviceOrientationService>();
#if ANDROID
            builder.Services.AddSingleton<ISettingsService, AndroidSettingsService>();
#elif IOS
            builder.Services.AddSingleton<ISettingsService, IosSettingsService>();
#endif
            builder.Services.AddSingleton<INavigationService, MauiNavigationService>();

            return builder.Build();
        }
    }
}
