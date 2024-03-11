using CommunityToolkit.Maui;
using LocalizationDemo.Services.Localization;
using LocalizationDemo.ViewModels;
using Microsoft.Extensions.Logging;

namespace LocalizationDemo
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<ILocalizer>(c => Localizer.Current);

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();


            //ResxTranslationProvider.Init(
            //    Strings.ResourceManager,
            //    () => Localizer.Current);

            //TranslateExtension.Init(
            //    () => Localizer.Current,
            //    () => ResxTranslationProvider.Current);

            return builder.Build();

        }
    }
}
