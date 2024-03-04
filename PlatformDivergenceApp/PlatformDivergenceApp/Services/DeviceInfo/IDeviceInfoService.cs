namespace PlatformDivergenceApp.Services.DeviceInfo
{
    public interface IDeviceInfoService
    {
        public string Model { get; }

        public string OSVersion { get; }

        public string AppVersion { get; }
    }
}