using PlatformDivergenceApp.Services;
using PlatformDivergenceApp.ViewModels;

namespace PlatformDivergenceApp.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        this.InitializeComponent();

        // Demo: Here we use a service locator to resolve the SettingsViewModel and its dependencies.
        //       Instead, we could inject SettingsViewModel via SettingsPage constructor.
        this.BindingContext = ServiceLocator.Current.GetRequiredService<SettingsViewModel>();
    }
}