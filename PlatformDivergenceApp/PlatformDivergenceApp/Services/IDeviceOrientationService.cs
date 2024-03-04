using InvokePlatformCodeDemos.Services;

namespace PlatformDivergenceApp.Services
{
    public interface IDeviceOrientationService
    {
        DeviceOrientation GetOrientation();
    }
}
