
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AppCenter.Crashes;
using Microsoft.Extensions.Logging;
using MonitoringDemo.Services;

namespace MonitoringDemo.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly ILogger<MainViewModel> logger;
        private readonly IAppCenterAnalytics appcenterAnalytics;
        private readonly ISentryAnalytics sentryAnalytics;
        private readonly IWorldTimeService worldTimeService;

        private ICommand divideCommand;
        private ICommand requestTimeZoneCommand;
        private decimal? dividend;
        private decimal? divisor;
        private decimal? quotient;
        private ICommand throwUnhandledExceptionCommand;
        private ICommand generateTestCrashCommand;
        private string timeZone;
        private string timeZoneInfo;

        public MainViewModel(
            ILogger<MainViewModel> logger,
            IAppCenterAnalytics appcenterAnalytics,
            ISentryAnalytics sentryAnalytics,
            IWorldTimeService worldTimeService)
        {
            this.logger = logger;
            this.appcenterAnalytics = appcenterAnalytics;
            this.sentryAnalytics = sentryAnalytics;
            this.worldTimeService = worldTimeService;

            this.TimeZone = "Europe/Zurich";
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
                this.appcenterAnalytics.TrackEvent("Divide", new Dictionary<string, string>
                {
                     { "Dividend", this.Dividend is decimal dividend ? $"{dividend}" : "null" },
                     { "Divisor", this.Divisor is decimal divisor ? $"{divisor}" : "null" },
                });

                this.sentryAnalytics.CaptureMessage("Divide");

                this.Quotient = this.Dividend / this.Divisor;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Divide with Dividend={this.Dividend} and Divisor={this.Divisor} failed with exception");
                this.appcenterAnalytics.TrackError(ex);
                this.sentryAnalytics.CaptureException(ex);
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
                this.appcenterAnalytics.TrackError(ex);
                this.sentryAnalytics.CaptureException(ex);
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

        public ICommand GenerateTestCrashCommand
        {
            get => this.generateTestCrashCommand ??= new Command(this.GenerateTestCrash);
        }

        private void GenerateTestCrash()
        {
            this.logger.LogDebug("GenerateTestCrash");

            Crashes.GenerateTestCrash();
        }

        public ICommand ThrowUnhandledExceptionCommand
        {
            get => this.throwUnhandledExceptionCommand ??= new Command(this.ThrowUnhandledException);
        }

        private void ThrowUnhandledException()
        {
            this.logger.LogDebug("ThrowUnhandledException");

            this.appcenterAnalytics.TrackEvent("Event 1");
            this.appcenterAnalytics.TrackEvent("Event 2");
            this.appcenterAnalytics.TrackEvent("Event 3");

            this.sentryAnalytics.AddBreadcrumb("Event 1");
            this.sentryAnalytics.AddBreadcrumb("Event 2");
            this.sentryAnalytics.AddBreadcrumb("Event 3");

            throw new InvalidOperationException("This is just a test exception", new NullReferenceException("Something cannot be null"));
        }
    }
}
