using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using LocalizationDemo.Services.Localization;
using Microsoft.Extensions.Logging;

namespace LocalizationDemo.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly ILogger<MainViewModel> logger;
        private readonly ILocalizer localizer;

        private Command throwUnhandledExceptionCommand;

        public MainViewModel(
            ILogger<MainViewModel> logger,
            ILocalizer localizer)
        {
            this.logger = logger;
            this.localizer = localizer;
        }

        public ICommand ThrowUnhandledExceptionCommand
        {
            get => this.throwUnhandledExceptionCommand ??= new Command(this.ThrowUnhandledException);
        }

        private void ThrowUnhandledException()
        {
            this.logger.LogDebug("ThrowUnhandledException");

            throw new InvalidOperationException("This is just a test exception", new NullReferenceException("Something cannot be null"));
        }
    }
}
