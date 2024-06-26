using Sentry.Extensibility;

namespace MonitoringDemo
{
    internal class SentryExceptionFilter : IExceptionFilter
    {
        private static Type[] FilteredExceptions = new[]
        {
            typeof(UnauthorizedAccessException),
            typeof(TaskCanceledException),
            typeof(TimeoutException),
        };

        public bool Filter(Exception exception)
        {
            var isFiltered = FilteredExceptions.Any(e => e.IsInstanceOfType(exception));
            return isFiltered;
        }
    }
}