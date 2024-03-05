using PlatformDivergenceApp.Services.DeviceInfo;
using PlatformDivergenceApp.ViewModels;

namespace PlatformDivergenceApp.Views;

public partial class DeviceInfoPage : ContentPage
{
    public DeviceInfoPage(DeviceInfoViewModel deviceInfoViewModel)
    {
        this.InitializeComponent();

        // Demo: DeviceInfoService can be create in platform-independent code or injected via DI.
        // IDeviceInfoService deviceInfoService = new DeviceInfoService();
        // var deviceInfoViewModel = new DeviceInfoViewModel(deviceInfoService);
        this.BindingContext = deviceInfoViewModel;
    }
}