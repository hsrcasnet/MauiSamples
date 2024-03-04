using PlatformDivergenceApp.Services.DeviceInfo;

namespace PlatformDivergenceApp.ViewModels
{
    public class DeviceInfoViewModel : ViewModelBase
    {
        public DeviceInfoViewModel(IDeviceInfoService deviceInfoService)
        {
            this.Model = deviceInfoService.Model;
            this.OSVersion = deviceInfoService.OSVersion;
            this.AppVersion = deviceInfoService.AppVersion;
        }

        public string Model { get; }

        public string OSVersion { get; }

        public string AppVersion { get; }
    }
}
