using PlatformDivergenceApp.Services;

namespace InvokePlatformCodeDemos.Services.PartialMethods
{
    public partial class DeviceOrientationService : IDeviceOrientationService
    {
        public partial DeviceOrientation GetOrientation();
    }
}
