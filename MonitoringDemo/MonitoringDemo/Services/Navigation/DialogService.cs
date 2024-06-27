using MonitoringDemo.Services.Analytics;

namespace MonitoringDemo.Services.Navigation
{
    public class DialogService : IDialogService
    {
        private readonly ISentryAnalytics sentryAnalytics;

        public DialogService(ISentryAnalytics sentryAnalytics)
        {
            this.sentryAnalytics = sentryAnalytics;
        }

        public Task DisplayAlertAsync(string title, string message, string accept)
        {
            this.sentryAnalytics.AddBreadcrumb("DisplayAlert");

            var currentPage = Application.Current.MainPage;
            return currentPage.DisplayAlert(title, message, accept);
        }

        public Task<bool> DisplayAlertAsync(string title, string message, string accept, string cancel)
        {
            this.sentryAnalytics.AddBreadcrumb("DisplayAlert");

            var currentPage = Application.Current.MainPage;
            return currentPage.DisplayAlert(title, message, accept, cancel);
        }
    }
}