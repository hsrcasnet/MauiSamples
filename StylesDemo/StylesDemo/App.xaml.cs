using StylesDemo.Views;

namespace StylesDemo
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
