namespace PlatformDivergenceApp.Services.Settings
{
    /// <summary>
    /// Settings base class which implements all platform-independent functionality.
    /// </summary>
    public abstract class SettingsServiceBase : ISettingsService
    {
        public void Reset(string key)
        {
            this.SetValue(key, null);
        }

        public abstract string GetValue(string key);

        public abstract void SetValue(string key, string newValue);
    }
}
