namespace PrismMauiApp.Platforms
{
    public static class PlatformInitializer
    {
        public static void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register Android-specific services.
            containerRegistry.RegisterSingleton<ISettingsService, AndroidSettingsService>();
        }
    }
}
