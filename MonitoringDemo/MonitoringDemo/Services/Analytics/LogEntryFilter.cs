using Microsoft.Extensions.Logging;
using Sentry.Extensions.Logging;

namespace MonitoringDemo.Services.Analytics
{
    internal class LogEntryFilter : ILogEntryFilter
    {
        private static readonly HashSet<string> FilteredCategoryNames = new HashSet<string>
        {
            "Microsoft.Maui.Controls.Xaml.Diagnostics.BindingDiagnostics"
        };

        public bool Filter(string categoryName, LogLevel logLevel, EventId eventId, Exception exception)
        {
            var isFiltered = FilteredCategoryNames.Contains(categoryName);
            return isFiltered;
        }
    }
}