namespace MauiTestingDemo.Services
{
    public class ServiceLocator : Microsoft.Maui.Hosting.IMauiInitializeService
    {
        public static IServiceProvider Services { get; private set; }

        public void Initialize(IServiceProvider services)
        {
            Services = services;
        }
    }
}
