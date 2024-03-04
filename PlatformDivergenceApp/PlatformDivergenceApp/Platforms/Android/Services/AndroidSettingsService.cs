using Android.Preferences;
using PlatformDivergenceApp.Services.Settings;
using Application = Android.App.Application;

#pragma warning disable CA1422

namespace PlatformDivergenceApp.Platforms.Services
{
    public class AndroidSettingsService : SettingsServiceBase, ISettingsService
    {
        public AndroidSettingsService()
        {
        }

        public override string GetValue(string key)
        {

            using (var sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(Application.Context))
            {
                return sharedPreferences.GetString(key, null);
            }
        }

        public override void SetValue(string key, string newValue)
        {
            using (var sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(Application.Context))
            {
                using (var sharedPreferencesEditor = sharedPreferences.Edit())
                {
                    sharedPreferencesEditor.PutString(key, newValue);
                    sharedPreferencesEditor.Commit();
                }
            }
        }
    }
}
