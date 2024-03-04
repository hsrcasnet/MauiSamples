using PlatformDivergenceApp.Views;

namespace PlatformDivergenceApp
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            this.InitializeComponent();

            //this.MainPage = new NavigationPage(new MainPage());

            var mainPage = serviceProvider.GetRequiredService<MainPage>();
            this.MainPage = new NavigationPage(mainPage);
        }
    }
}
