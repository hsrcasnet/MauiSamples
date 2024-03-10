using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiTestingDemo.ViewModels
{
    public class TodoListViewModel : ObservableObject
    {
        private readonly ILogger<TodoListViewModel> logger;

        private IAsyncRelayCommand incrementCounterCommand;
        private int count;

        public TodoListViewModel(ILogger<TodoListViewModel> logger)
        {
            this.logger = logger;
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
            get => this.Count == 0 ? "Click me" : $"Clicked {this.Count} times";
        }

        public IAsyncRelayCommand IncrementCounterCommand
        {
            get => this.incrementCounterCommand ??= new AsyncRelayCommand(this.IncrementCounterAsync);
        }

        private async Task IncrementCounterAsync()
        {
            this.logger.LogDebug("IncrementCounter");

            await Task.Delay(1000);

            this.Count++;
        }
    }
}
