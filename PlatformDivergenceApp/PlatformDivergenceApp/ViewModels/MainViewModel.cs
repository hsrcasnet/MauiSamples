using System.Windows.Input;
using PlatformDivergenceApp.Services;
using PlatformDivergenceApp.Views;

namespace PlatformDivergenceApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;

        public MainViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            this.NavigateToSettingsPageCommand = new Command(
                execute: this.NavigateToSettingsPageAsync);

            this.NavigateToDeviceOrientationPageCommand = new Command(
                execute: this.NavigateToDeviceOrientationPageAsync);

            this.NavigateToDeviceInfoPageCommand = new Command(
                execute: this.NavigateToDeviceInfoPageAsync);

            this.NavigateToPlatformSpecificPageCommand = new Command(
                execute: this.NavigateToPlatformSpecificPageAsync);
        }

        public ICommand NavigateToSettingsPageCommand { get; }

        private async void NavigateToSettingsPageAsync()
        {
            await this.navigationService.PushAsync<SettingsPage>();
        }

        public ICommand NavigateToDeviceOrientationPageCommand { get; }

        private async void NavigateToDeviceOrientationPageAsync()
        {
            await this.navigationService.PushAsync<DeviceOrientationPage>();
        }

        public ICommand NavigateToDeviceInfoPageCommand { get; }

        private async void NavigateToDeviceInfoPageAsync()
        {
            await this.navigationService.PushAsync<DeviceInfoPage>();
        }

        public ICommand NavigateToPlatformSpecificPageCommand { get; }

        private async void NavigateToPlatformSpecificPageAsync()
        {
            await this.navigationService.PushAsync<PlatformSpecificPage>();
        }
    }
}
