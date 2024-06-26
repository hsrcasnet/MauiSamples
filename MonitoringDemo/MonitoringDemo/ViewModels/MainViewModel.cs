using System.Text.RegularExpressions;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using MonitoringDemo.Services;

namespace MonitoringDemo.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private const string MessagePreferencesKey = "MainViewModel.Message";
        private const string ExceptionMessagePreferencesKey = "MainViewModel.ExceptionMessage";

        private static readonly IDictionary<Type, Func<string, Exception>> ExceptionFactories = new Dictionary<Type, Func<string, Exception>>
        {
            { typeof(Exception),  s => new Exception(s) },
            { typeof(ArgumentException),  s => new ArgumentException(s, "testParam") },
            { typeof(ArgumentNullException),  s => new ArgumentNullException("testParam", s) },
            { typeof(ArgumentOutOfRangeException),  s => new ArgumentOutOfRangeException("testParam", s) },
            { typeof(IndexOutOfRangeException),  s => new IndexOutOfRangeException(s) },
            { typeof(InvalidOperationException),  s => new InvalidOperationException(s) },
            { typeof(NotImplementedException),  s => new NotImplementedException(s) },
            { typeof(TaskCanceledException),  s => new TaskCanceledException(s) },
            { typeof(TimeoutException),  s => new TimeoutException(s) },
            { typeof(OperationCanceledException),  s => new OperationCanceledException(s) },
            { typeof(OutOfMemoryException),  s => new OutOfMemoryException(s) },
            { typeof(ObjectDisposedException),  s => new ObjectDisposedException(s) },
            { typeof(UnauthorizedAccessException),  s => new UnauthorizedAccessException(s) },
        };

        private readonly ILogger<MainViewModel> logger;
        private readonly IPreferences preferences;
        private readonly ISentryAnalytics sentryAnalytics;
        private readonly IWorldTimeService worldTimeService;

        private ICommand divideCommand;
        private ICommand requestTimeZoneCommand;
        private decimal? dividend;
        private decimal? divisor;
        private decimal? quotient;
        private string timeZone;
        private string timeZoneInfo;

        private ICommand logErrorCommand;
        private string message;
        private string exceptionMessage;
        private ICommand logCommand;
        private LogLevel logLevel;
        private ICommand captureExceptionCommand;
        private ICommand throwUnhandledExceptionCommand;
        private ICommand generateTestCrashCommand;
        private string exceptionName;

        public MainViewModel(
            ILogger<MainViewModel> logger,
            IPreferences preferences,
            ISentryAnalytics sentryAnalytics,
            IWorldTimeService worldTimeService)
        {
            this.logger = logger;
            this.preferences = preferences;
            this.sentryAnalytics = sentryAnalytics;
            this.worldTimeService = worldTimeService;

            this.TimeZone = "Europe/Zurich";

            var lastMessage = this.preferences.Get<string>(MessagePreferencesKey, null);
            this.message = lastMessage ?? "Test log message (1)";

            var lastExceptionMessage = this.preferences.Get<string>(ExceptionMessagePreferencesKey, null);
            this.exceptionMessage = lastExceptionMessage ?? "Test exception message (1)";

            this.LogLevels = Enum.GetValues(typeof(LogLevel))
                .Cast<LogLevel>()
                .ToArray();
            this.LogLevel = LogLevel.Information;

            this.ExceptionNames = ExceptionFactories
                .Select(f => f.Key.FullName)
                .OrderBy(n => n)
                .ToArray();
            this.ExceptionName = this.ExceptionNames.FirstOrDefault();
        }

        public decimal? Dividend
        {
            get => this.dividend;
            set => this.SetProperty(ref this.dividend, value);
        }

        public decimal? Divisor
        {
            get => this.divisor;
            set => this.SetProperty(ref this.divisor, value);
        }

        public decimal? Quotient
        {
            get => this.quotient;
            private set => this.SetProperty(ref this.quotient, value);
        }

        public ICommand DivideCommand
        {
            get => this.divideCommand ??= new Command(this.Divide);
        }

        private void Divide()
        {
            this.logger.LogDebug("Divide");

            try
            {
                var additionalProperties = new Dictionary<string, string>
                {
                     { "Dividend", this.Dividend is decimal dividend ? $"{dividend}" : "null" },
                     { "Divisor", this.Divisor is decimal divisor ? $"{divisor}" : "null" },
                };

                this.sentryAnalytics.CaptureMessage(
                    message: "Divide",
                    sentryLevel: SentryLevel.Debug,
                    data: additionalProperties);

                this.Quotient = this.Dividend / this.Divisor;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex,
                    $"Divide with Dividend={this.Dividend} and Divisor={this.Divisor} " +
                    $"failed with exception");

                this.Quotient = null;
            }
        }

        public ICommand RequestTimeZoneCommand
        {
            get => this.requestTimeZoneCommand ??= new Command(this.RequestTimeZone);
        }

        private async void RequestTimeZone()
        {
            this.logger.LogDebug("RequestTimeZone");

            try
            {
                var timeZoneInfo = await this.worldTimeService.GetTimeZoneInfoAsync(this.TimeZone);
                this.TimeZoneInfo = timeZoneInfo;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "RequestTimeZone failed with exception");

                this.TimeZoneInfo = ex.Message;
            }
        }

        public string TimeZone
        {
            get => this.timeZone;
            set => this.SetProperty(ref this.timeZone, value);
        }

        public string TimeZoneInfo
        {
            get => this.timeZoneInfo;
            private set => this.SetProperty(ref this.timeZoneInfo, value);
        }

        public string Message
        {
            get => this.message;
            set
            {
                if (this.SetProperty(ref this.message, value))
                {
                    this.preferences.Set(MessagePreferencesKey, value);
                }
            }
        }

        public IEnumerable<LogLevel> LogLevels { get; private set; }

        public LogLevel LogLevel
        {
            get => this.logLevel;
            set => this.SetProperty(ref this.logLevel, value);
        }

        public ICommand LogMessageCommand => this.logCommand ??= new RelayCommand(this.LogMessage);

        private void LogMessage()
        {
            this.logger.Log(this.LogLevel, this.Message);

            TryIncrementNumberInProperty(s => this.Message = s, () => this.Message);
        }

        public string ExceptionMessage
        {
            get => this.exceptionMessage;
            set
            {
                if (this.SetProperty(ref this.exceptionMessage, value))
                {
                    this.preferences.Set(ExceptionMessagePreferencesKey, value);
                }
            }
        }

        public IEnumerable<string> ExceptionNames { get; private set; }

        public string ExceptionName
        {
            get => this.exceptionName;
            set => this.SetProperty(ref this.exceptionName, value);
        }

        public ICommand LogErrorCommand => this.logErrorCommand ??= new RelayCommand(this.LogError);

        private void LogError()
        {
            var exception = CreateException(this.ExceptionName, this.ExceptionMessage);

            this.logger.LogError(exception, exception.Message);

            TryIncrementNumberInProperty(s => this.ExceptionMessage = s, () => this.ExceptionMessage);
        }

        private static Exception CreateException(string exceptionName, string exceptionMessage)
        {
            return ExceptionFactories
                .Single(f => f.Key.FullName == exceptionName)
                .Value(exceptionMessage);
        }

        private static void TryIncrementNumberInProperty(Action<string> setProperty, Func<string> getProperty)
        {
            if (getProperty() is string propertyValue)
            {
                var match = Regex.Match(propertyValue, "(?<Number>[0-9]+)[^0-9]*$");
                if (match.Success &&
                    match.Groups.TryGetValue("Number", out var numberGroup))
                {
                    if (int.TryParse(numberGroup.Value, out var number))
                    {
                        setProperty(propertyValue.Replace(numberGroup.Value, $"{++number}"));
                    }
                }
            }
        }

        public ICommand CaptureExceptionCommand => this.captureExceptionCommand ??= new RelayCommand(this.CaptureException);

        private void CaptureException()
        {
            this.sentryAnalytics.AddBreadcrumb("Event 1");
            this.sentryAnalytics.AddBreadcrumb("Event 2");
            this.sentryAnalytics.AddBreadcrumb("Event 3");

            var exception = CreateException(this.ExceptionName, this.ExceptionMessage);

            TryIncrementNumberInProperty(s => this.ExceptionMessage = s, () => this.ExceptionMessage);

            this.sentryAnalytics.CaptureException(exception);
        }
        
        public ICommand ThrowUnhandledExceptionCommand => this.throwUnhandledExceptionCommand ??= new RelayCommand(this.ThrowUnhandledException);

        private void ThrowUnhandledException()
        {
            this.sentryAnalytics.AddBreadcrumb("Event 1");
            this.sentryAnalytics.AddBreadcrumb("Event 2");
            this.sentryAnalytics.AddBreadcrumb("Event 3");

            var exception = CreateException(this.ExceptionName, this.ExceptionMessage);

            TryIncrementNumberInProperty(s => this.ExceptionMessage = s, () => this.ExceptionMessage);

            throw exception;
        }

        public ICommand GenerateTestCrashCommand
        {
            get => this.generateTestCrashCommand ??= new Command(this.GenerateTestCrash);
        }

        private void GenerateTestCrash()
        {
            this.logger.LogDebug("GenerateTestCrash");

            SentrySdk.CauseCrash(CrashType.Managed);
        }
    }
}
