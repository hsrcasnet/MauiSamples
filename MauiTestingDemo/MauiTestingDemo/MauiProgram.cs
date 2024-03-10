using MauiTestingDemo.Services;
using MauiTestingDemo.ViewModels;
using Microsoft.Extensions.Logging;

namespace MauiTestingDemo
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

            builder.Services.AddSingleton<IMauiInitializeService, ServiceLocator>();

            builder.Services.AddTransient<TodoListPage>();
            builder.Services.AddTransient<TodoListViewModel>();

            return builder.Build();
        }
    }
}
