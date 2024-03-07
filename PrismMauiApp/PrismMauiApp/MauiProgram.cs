using Microsoft.Extensions.Logging;
using PrismMauiApp.Platforms;
using PrismMauiApp.Services;
using PrismMauiApp.ViewModels;
using PrismMauiApp.Views;

namespace PrismMauiApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UsePrism(prism =>
                {
                    prism
                        .RegisterTypes(RegisterTypes)
#if ANDROID || IOS
                        .RegisterTypes(PlatformInitializer.RegisterTypes)
#endif
                        .CreateWindow(async (c, navigationService) =>
                        {
                            await navigationService.NavigateAsync($"/{App.Pages.NavigationPage}/{App.Pages.TodoListPage}");
                        });
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register platform-independent services
            containerRegistry.RegisterSingleton<ITodoRepository, TodoRepositoryMock>();
            containerRegistry.RegisterSingleton<ILauncher>(() => Launcher.Default);

            // Register pages and viewmodels
            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>(App.Pages.MainPage);
            containerRegistry.RegisterForNavigation<TodoListPage, TodoListViewModel>(App.Pages.TodoListPage);
            containerRegistry.RegisterForNavigation<TodoDetailPage, TodoDetailViewModel>(App.Pages.TodoDetailPage);
            containerRegistry.RegisterForNavigation<NewTodoPage, TodoDetailViewModel>(App.Pages.NewTodoPage);
        }
    }
}
