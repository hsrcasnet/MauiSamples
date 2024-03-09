using Windows.Storage;

namespace PlatformDivergenceApp.Services.Settings
{
    public class WindowsSettingsService : SettingsServiceBase, ISettingsService
    {
        private readonly ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        public WindowsSettingsService()
        {
        }

        public override string GetValue(string key)
        {
            return this.localSettings.Values[key]?.ToString();
        }

        public override void SetValue(string key, string newValue)
        {
            this.localSettings.Values[key] = newValue;
        }
    }
}
