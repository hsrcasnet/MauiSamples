namespace PrismMauiApp.Platforms
{
    public static class PlatformInitializer
    {
        public static void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register iOS-specific services.
            //containerRegistry.RegisterSingleton<ISettingsService, IosSettingsService>();
        }
    }
}
