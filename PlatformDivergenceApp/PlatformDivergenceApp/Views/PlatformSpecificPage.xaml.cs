using PlatformDivergenceApp.ViewModels;

namespace PlatformDivergenceApp.Views
{
    public partial class PlatformSpecificPage : ContentPage
    {
        public PlatformSpecificPage(PlatformSpecificViewModel platformSpecificViewModel)
        {
            this.InitializeComponent();
            this.BindingContext = platformSpecificViewModel;
        }
    }
}