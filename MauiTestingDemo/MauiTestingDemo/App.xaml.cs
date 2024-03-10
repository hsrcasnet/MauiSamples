using MauiTestingDemo.Services;

namespace MauiTestingDemo
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();

            // We're using a service locator to resolve the root page:
            var calculatorPage = ServiceLocator.Services.GetRequiredService<CalculatorPage>();

            // Yet another approach of resolving the root page:
            //var calculatorPage = IPlatformApplication.Current?.Services.GetRequiredService<CalculatorPage>();

            
            this.MainPage = new NavigationPage(calculatorPage);
        }
    }
}
