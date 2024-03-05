using PlatformDivergenceApp.Services.Orientation;
using PlatformDivergenceApp.ViewModels;

namespace PlatformDivergenceApp.Views;

public partial class DeviceOrientationPage : ContentPage
{
    public DeviceOrientationPage(DeviceOrientationViewModel deviceOrientationViewModel)
    {
        this.InitializeComponent();

        // Demo: DeviceInfoService can be create in platform-independent code or injected via DI.
        //IDeviceOrientationService deviceOrientationService = new DeviceOrientationService();
        //var deviceOrientationViewModel = new DeviceOrientationViewModel(deviceOrientationService);
        this.BindingContext = deviceOrientationViewModel;
    }
}