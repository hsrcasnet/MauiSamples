using System.IO;
using System.Text;
using AppCenterDemo.Views;
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
            // Demo: AppCenter configuration at startup
            Crashes.ShouldProcessErrorReport = errorReport =>
            {
                return true;
            };

            Crashes.ShouldAwaitUserConfirmation = () =>
            {
                return false;
            };

            Crashes.GetErrorAttachments = errorReport =>
            {
                // Demo: Read log file content here
                var logFileContent = $"{DateTime.UtcNow:O} - ERROR - Something went wrong";
                var errorAttachmentLog = ErrorAttachmentLog.AttachmentWithText(logFileContent, $"logfile-{DateTime.UtcNow:u}.log");
                return new [] { errorAttachmentLog };
            };

            // Demo: Set user ID for better error tracking.
            //       Don't use e-mail addresses or any other personal information here!
            AppCenter.SetUserId(Guid.NewGuid().ToString());

            AppCenter.Start(
               $"ios=ad52a6d1-2e81-4af1-9398-54e53603311e;" +
               $"android=c45fb280-e432-44cb-8b1c-eefa1c63fbd4;" +
               $"uwp=ec8ae0f2-5fb2-4fc3-8cdd-c1bcf37d243c;",
               typeof(Analytics),
               typeof(Crashes));
        }
    }
}
