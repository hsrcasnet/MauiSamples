using PlatformDivergenceApp.ViewModels;

namespace PlatformDivergenceApp.Views;

public partial class DeviceOrientationPage : ContentPage
{
    public DeviceOrientationPage(DeviceOrientationViewModel deviceOrientationViewModel)
    {
        this.InitializeComponent();
        this.BindingContext = deviceOrientationViewModel;
    }
}