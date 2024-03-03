using ForexApp.Views;

namespace MauiForexApp
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();

            this.MainPage = new NavigationPage(new MainPage());
        }
    }
}
