using PlatformDivergenceApp.Services.DeviceInfo;

namespace PlatformDivergenceApp.ViewModels
{
    public class DeviceInfoViewModel : ViewModelBase
    {
        public DeviceInfoViewModel(IDeviceInfoService deviceInfoService)
        {
            this.Title = GetPlatformSpecificTitle();

            this.Model = deviceInfoService.Model;
            this.OSVersion = deviceInfoService.OSVersion;
            this.AppVersion = deviceInfoService.AppVersion;
        }

        private static string GetPlatformSpecificTitle()
        {
            // Demo: DevicePlatform can be used differentiate between runtime platforms.
            var currentPlatform = DeviceInfo.Current.Platform;
            if (currentPlatform == DevicePlatform.iOS)
            {
                return "iOS Device Info";
            }
            else if (currentPlatform == DevicePlatform.Android)
            {
                return "Android Device Info";
            }

            throw new PlatformNotSupportedException($"GetPlatformSpecificTitle does currently not support platform {currentPlatform}");
        }


        public string Title { get; }

        public string Model { get; }

        public string OSVersion { get; }

        public string AppVersion { get; }
    }
}
