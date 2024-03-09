using Microsoft.Maui.Dispatching;
using Windows.Graphics.Display;
using Windows.UI.Core;
using Windows.UI.ViewManagement;

namespace PlatformDivergenceApp.Services.Orientation
{
    public partial class DeviceOrientationService
    {
        public partial DeviceOrientation GetOrientation()
        {
            return DeviceOrientation.Landscape;
            //throw new NotImplementedException();
        }
    }
}
