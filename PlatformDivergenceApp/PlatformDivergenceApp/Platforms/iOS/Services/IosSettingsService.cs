using Foundation;
using PlatformDivergenceApp.Services.Settings;

namespace PlatformDivergenceApp.Platforms.Services
{
    public class IosSettingsService : SettingsServiceBase, ISettingsService
    {
        public IosSettingsService()
        {
        }

        public override string GetValue(string key)
        {
            using (var defaults = NSUserDefaults.StandardUserDefaults)
            {
                return defaults.StringForKey(key);
            }
        }

        public override void SetValue(string key, string newValue)
        {
            using (var defaults = NSUserDefaults.StandardUserDefaults)
            {
                defaults.SetString(newValue ?? string.Empty, key);
                defaults.Synchronize();
            }
        }
    }
}
