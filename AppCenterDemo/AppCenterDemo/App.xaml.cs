using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace AppCenterDemo
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();

            var mainPage = IPlatformApplication.Current?.Services.GetRequiredService<MainPage>();
            this.MainPage = new NavigationPage(mainPage);
        }

        protected override void OnStart()
        {
            AppCenter.Start(
               $"ios=ad52a6d1-2e81-4af1-9398-54e53603311e;" +
               $"android=c45fb280-e432-44cb-8b1c-eefa1c63fbd4;" +
               $"uwp=ec8ae0f2-5fb2-4fc3-8cdd-c1bcf37d243c;" +
               typeof(Analytics),
               typeof(Crashes));
        }
    }
}
