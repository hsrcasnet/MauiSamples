using PagesLayoutsControlsDemo.Views;

namespace PagesLayoutsControlsDemo
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();

            this.MainPage = new FlyoutDemoPage();
        }

        protected override void OnStart()
        {
            // Called when app is started
        }

        protected override void OnSleep()
        {
            // Called when app is going to background
        }

        protected override void OnResume()
        {
            // Called when app is resumed from background
        }
    }
}
