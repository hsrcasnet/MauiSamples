using System.Windows.Input;
using AppCenterDemo.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;

namespace AppCenterDemo.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly ILogger<MainViewModel> logger;
        private readonly IAnalytics analytics;

        private Command divideCommand;
        private decimal dividend;
        private decimal divisor;
        private decimal? quotient;

        public MainViewModel(
            ILogger<MainViewModel> logger,
            IAnalytics analytics)
        {
            this.logger = logger;
            this.analytics = analytics;
        }

        public ICommand DivideCommand
        {
            get => this.divideCommand ??= new Command(this.Divide);
        }

        public decimal Dividend
        {
            get => this.dividend;
            set => this.SetProperty(ref this.dividend, value);
        }

        public decimal Divisor
        {
            get => this.divisor;
            set => this.SetProperty(ref this.divisor, value);
        }

        public decimal? Quotient
        {
            get => this.quotient;
            private set => this.SetProperty(ref this.quotient, value);
        }

        private void Divide()
        {
            this.logger.LogDebug("Divide");

            try
            {
                this.analytics.TrackEvent("Divide", new Dictionary<string, string>
                {
                     { "Dividend", $"{this.Dividend}" },
                     { "Divisor", $"{this.Divisor}" },
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
    }
}
