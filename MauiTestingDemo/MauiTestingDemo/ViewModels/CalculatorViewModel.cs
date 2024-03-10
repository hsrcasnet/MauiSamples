using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiTestingDemo.Services;

namespace MauiTestingDemo.ViewModels
{
    public class CalculatorViewModel : ObservableObject
    {
        private readonly ILogger<CalculatorViewModel> logger;
        private readonly ICalculatorService calculatorService;

        private IAsyncRelayCommand incrementCounterCommand;
        private int count;
        private IRelayCommand calculateSumCommand;
        private decimal summand1;
        private decimal summand2;
        private string sumResult;

        public CalculatorViewModel(
            ILogger<CalculatorViewModel> logger,
            ICalculatorService calculatorService)
        {
            this.logger = logger;
            this.calculatorService = calculatorService;
        }

        public int Count
        {
            get => this.count;
            private set
            {
                if (this.SetProperty(ref this.count, value))
                {
                    this.OnPropertyChanged(nameof(this.CountButtonText));
                }
            }
        }

        public string CountButtonText
        {
            get
            {
                if (this.Count == 0)
                {
                    return "Click me";
                }

                return $"Clicked {this.Count} time{(this.Count == 1 ? "" : "s")}";
            }
        }

        public IAsyncRelayCommand IncrementCounterCommand
        {
            get => this.incrementCounterCommand ??= new AsyncRelayCommand(this.IncrementCounterAsync);
        }

        private async Task IncrementCounterAsync()
        {
            this.logger.LogDebug("IncrementCounter");

            await Task.Delay(1000);

            this.Count = this.calculatorService.Increment(this.Count);
            ;
        }

        public decimal Summand1
        {
            get => this.summand1;
            set => this.SetProperty(ref this.summand1, value);
        }

        public decimal Summand2
        {
            get => this.summand2;
            set => this.SetProperty(ref this.summand2, value);
        }

        public string SumResult
        {
            get => this.sumResult;
            set => this.SetProperty(ref this.sumResult, value);
        }

        public IRelayCommand CalculateSumCommand
        {
            get => this.calculateSumCommand ??= new RelayCommand(this.CalculateSum);
        }

        private void CalculateSum()
        {
            this.logger.LogDebug("Sum");

            var sum = this.calculatorService.Sum(this.Summand1, this.Summand2);
            this.SumResult = sum.ToString("0.00");
        }
    }
}
