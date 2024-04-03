using Microsoft.Extensions.Logging;
using PlatformDivergenceApp.Services;
using PlatformDivergenceApp.Services.DeviceInfo;
using PlatformDivergenceApp.Services.Orientation;
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

            builder.Services.AddTransient<DeviceInfoPage>();
            builder.Services.AddTransient<DeviceInfoViewModel>();

            builder.Services.AddTransient<PlatformSpecificPage>();
            builder.Services.AddTransient<PlatformSpecificViewModel>();

            // Register services
            builder.Services.AddSingleton<INavigationService, MauiNavigationService>();
            builder.Services.AddSingleton<IDeviceOrientationService, DeviceOrientationService>();
            builder.Services.AddSingleton<IDeviceInfoService, DeviceInfoService>();

#if ANDROID
            builder.Services.AddSingleton<ISettingsService, AndroidSettingsService>();
#elif IOS
            builder.Services.AddSingleton<ISettingsService, IosSettingsService>();
#elif WINDOWS
            builder.Services.AddSingleton<ISettingsService, WindowsSettingsService>();
#endif

            return builder.Build();
        }
    }
}
