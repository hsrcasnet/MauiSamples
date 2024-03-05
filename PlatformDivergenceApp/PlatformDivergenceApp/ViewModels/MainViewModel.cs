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
        }

        public ICommand NavigateToSettingsPageCommand { get; set; }

        private async void NavigateToSettingsPageAsync()
        {
            await this.navigationService.PushAsync<SettingsPage>();
        }

        public ICommand NavigateToDeviceOrientationPageCommand { get; set; }

        private async void NavigateToDeviceOrientationPageAsync()
        {
            await this.navigationService.PushAsync<DeviceOrientationPage>();
        }
        public ICommand NavigateToDeviceInfoPageCommand { get; set; }

        private async void NavigateToDeviceInfoPageAsync()
        {
            await this.navigationService.PushAsync<DeviceInfoPage>();
        }
    }
}
