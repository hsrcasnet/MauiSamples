namespace MonitoringDemo.Services.Analytics
{
    public static class SentryAnalyticsExtensions
    {
        public static void TrackDivide(this ISentryAnalytics sentryAnalytics, decimal? dividend, decimal? divisor, decimal? quotient)
        {
            var properties = new Dictionary<string, string>
            {
                    { "Dividend", dividend is decimal x ? $"{x}" : "null" },
                    { "Divisor", divisor is decimal y ? $"{y}" : "null" },
                    { "Quotient", quotient is decimal q ? $"{q}" : "null" },
            };

            sentryAnalytics.MetricsIncrement("Divide", properties);
        }

        public static void TrackPageView(this ISentryAnalytics sentryAnalytics, string pageName)
        {
            var properties = new Dictionary<string, string>
            {
                { "PageName", pageName }
            };

            sentryAnalytics.MetricsIncrement("PageView", properties);
            sentryAnalytics.MetricsIncrement($"PageView {pageName}");
        }
    }
}
