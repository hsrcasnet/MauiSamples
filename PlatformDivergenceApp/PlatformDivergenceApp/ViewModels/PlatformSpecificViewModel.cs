using System.Windows.Input;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;

namespace PlatformDivergenceApp.ViewModels
{
    public class PlatformSpecificViewModel : ViewModelBase
    {
        public PlatformSpecificViewModel(ILogger<PlatformSpecificViewModel> logger)
        {
            var windowSoftInputModeAdjust = App.Current.On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().GetWindowSoftInputModeAdjust();
            logger.LogDebug($"windowSoftInputModeAdjust={windowSoftInputModeAdjust}");

            this.SetPanCommand = new Command(execute: this.SetPan);
            this.SetResizeCommand = new Command(execute: this.SetResize);
        }

        public ICommand SetPanCommand { get; }

        public void SetPan()
        {
            App.Current.On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Pan);
        }

        public ICommand SetResizeCommand { get; }

        public void SetResize()
        {
            App.Current.On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
        }
    }
}
