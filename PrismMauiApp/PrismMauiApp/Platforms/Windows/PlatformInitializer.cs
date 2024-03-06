namespace PrismMauiApp.Platforms
{
    public static class PlatformInitializer
    {
        public static void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register Windows-specific services.

            //containerRegistry.RegisterSingleton<HttpMessageHandler, WindowsMessageHandler>();
            //containerRegistry.RegisterSingleton<IWifiConnector, WifiConnector>();
        }
    }
}
