namespace PlatformDivergenceApp.Services.Settings
{
    /// <summary>
    /// Platform-agnostic settings interface to read and write key/value pairs of strings.
    /// </summary>
    public interface ISettingsService
    {
        string GetValue(string key);

        void SetValue(string key, string newValue);

        void Reset(string key);
    }
}