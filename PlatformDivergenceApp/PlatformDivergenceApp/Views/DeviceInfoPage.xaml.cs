using PlatformDivergenceApp.ViewModels;

namespace PlatformDivergenceApp.Views;

public partial class DeviceInfoPage : ContentPage
{
    public DeviceInfoPage(DeviceInfoViewModel deviceInfoViewModel)
    {
        this.InitializeComponent();
        this.BindingContext = deviceInfoViewModel;

    }
}