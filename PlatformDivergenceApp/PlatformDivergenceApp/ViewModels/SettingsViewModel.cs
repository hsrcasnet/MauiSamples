using System.Windows.Input;
using PlatformDivergenceApp.Services.Settings;

namespace PlatformDivergenceApp.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly ISettingsService settingsService;
        private string settingsValue;

        public SettingsViewModel(ISettingsService settingsService)
        {
            this.settingsService = settingsService;

            this.GetValueCommand = new Command(execute: this.GetValue);
            this.SetValueCommand = new Command(execute: this.SetValue);
            this.ResetValueCommand = new Command(execute: this.ResetValue);
        }

        public string SettingsValue
        {
            get => this.settingsValue;
            set => this.SetProperty(ref this.settingsValue, value);
        }

        public ICommand GetValueCommand { get; }

        private void GetValue()
        {
            var value = this.settingsService.GetValue("SettingsKey");
            this.SettingsValue = value;
        }

        public ICommand SetValueCommand { get; }

        private void SetValue()
        {
            var value = this.SettingsValue;
            this.settingsService.SetValue("SettingsKey", value);
        }

        public ICommand ResetValueCommand { get; }

        private void ResetValue()
        {
            this.settingsService.Reset("SettingsKey");
            this.SettingsValue = this.settingsService.GetValue("SettingsKey");
        }
    }
}
