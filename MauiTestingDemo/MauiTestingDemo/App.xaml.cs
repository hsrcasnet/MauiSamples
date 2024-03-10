using MauiTestingDemo.Services;

namespace MauiTestingDemo
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();

            var page = ServiceLocator.Services.GetRequiredService<TodoListPage>();
            this.MainPage = new NavigationPage(page);
        }
    }
}
