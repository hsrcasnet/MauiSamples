using System.Windows.Input;
using InvokePlatformCodeDemos.Services;
using PlatformDivergenceApp.Services;

namespace PlatformDivergenceApp.ViewModels
{
    public class DeviceOrientationViewModel : ViewModelBase
    {
        private readonly IDeviceOrientationService deviceOrientationService;

        private DeviceOrientation deviceOrientation;

        public DeviceOrientationViewModel(IDeviceOrientationService deviceOrientationService)
        {
            this.deviceOrientationService = deviceOrientationService;

            this.RefreshDeviceOrientationCommand = new Command(execute: this.RefreshDeviceOrientation);
            this.RefreshDeviceOrientationCommand.Execute(null);
        }

        public DeviceOrientation DeviceOrientation
        {
            get => this.deviceOrientation;
            private set => this.SetProperty(ref this.deviceOrientation, value);
        }

        public ICommand RefreshDeviceOrientationCommand { get; set; }

        private void RefreshDeviceOrientation()
        {
            this.DeviceOrientation = this.deviceOrientationService.GetOrientation();
        }
    }
}
