using System.Windows.Input;
using AppCenterDemo.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AppCenter.Crashes;
using Microsoft.Extensions.Logging;

namespace AppCenterDemo.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly ILogger<MainViewModel> logger;
        private readonly IAnalytics analytics;

        private Command divideCommand;
        private decimal? dividend;
        private decimal? divisor;
        private decimal? quotient;
        private ICommand throwUnhandledExceptionCommand;
        private ICommand generateTestCrashCommand;

        public MainViewModel(
            ILogger<MainViewModel> logger,
            IAnalytics analytics)
        {
            this.logger = logger;
            this.analytics = analytics;
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
                this.analytics.TrackEvent("Divide", new Dictionary<string, string>
                {
                     { "Dividend", this.Dividend is decimal dividend ? $"{dividend}" : "null" },
                     { "Divisor", this.Divisor is decimal divisor ? $"{divisor}" : "null" },
                });

                this.Quotient = this.Dividend / this.Divisor;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Divide with Dividend={this.Dividend} and Divisor={this.Divisor} failed with exception");
                this.analytics.TrackError(ex);
                this.Quotient = null;
            }
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

            this.analytics.TrackEvent("Event 1");
            this.analytics.TrackEvent("Event 2");
            this.analytics.TrackEvent("Event 3");

            throw new InvalidOperationException("This is just a test exception", new NullReferenceException("Something cannot be null"));
        }
    }
}
