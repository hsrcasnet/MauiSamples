using XamlDemo.Views;

namespace XamlDemo
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();

            // DEMO: Use MainPage XAML or C#
            //this.MainPage = new MainPage();
            this.MainPage = new MainPageCsharp();
        }
    }
}
