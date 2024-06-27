using System.Reflection;
using Microsoft.Extensions.Logging;
using MonitoringDemo.Services.Analytics;

namespace MonitoringDemo.Services.Navigation
{
    public class MauiNavigationService : INavigationService
    {
        private readonly ILogger logger;
        private readonly IServiceProvider serviceProvider;
        private readonly ISentryAnalytics sentryAnalytics;

        public MauiNavigationService(
            ILogger<MauiNavigationService> logger,
            IServiceProvider serviceProvider,
            ISentryAnalytics sentryAnalytics)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
            this.sentryAnalytics = sentryAnalytics;
        }

        public async Task PushAsync(string pageName)
        {
            // Use extension method send simple tracking events
            this.sentryAnalytics.TrackPageView(pageName);

            // Use transaction with spans to report fine-granular performance values
            var transaction = this.sentryAnalytics.StartTransaction("PushAsync", pageName);

            try
            {
                var getPageTypeSpan = transaction.StartChild("get_page_type", "Get page type");
                var pageTypes = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(t => string.Equals(t.Name, pageName, StringComparison.InvariantCultureIgnoreCase))
                    .ToArray();

                if (pageTypes.Length == 0)
                {
                    throw new PageNavigationException($"Page with name '{pageName}' not found");
                }

                if (pageTypes.Length > 1)
                {
                    throw new PageNavigationException($"Multiple pages found for name '{pageName}': {string.Join($"> {Environment.NewLine}", pageTypes.Select(t => t.FullName))}");
                }

                var pageType = pageTypes.Single();
                getPageTypeSpan.Finish();

                var resolvePageSpan = transaction.StartChild("resolve_page", "Resolve page");
                var page = (Page)this.serviceProvider.GetRequiredService(pageType);
                resolvePageSpan.Finish();

                var navigatePageSpan = transaction.StartChild("navigate_to_page", "Navigate to page");
                await Application.Current.MainPage.Navigation.PushAsync(page);
                navigatePageSpan.Finish();

                transaction.Finish();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "PushAsync failed with exception");
                transaction.Finish(ex);
                throw;
            }
        }

        public async Task PopAsync()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "PopAsync failed with exception");
                throw;
            }
        }
    }
}