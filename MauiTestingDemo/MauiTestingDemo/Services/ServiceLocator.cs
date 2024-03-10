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

    // Yet another service locator approach:

//    public static class ServiceLocator
//    {
//        public static TService GetService<TService>()
//            => Current.GetService<TService>();

//        public static IServiceProvider Current
//            =>
//#if WINDOWS
//			MauiWinUIApplication.Current.Services;
//#elif ANDROID
//            MauiApplication.Current.Services;
//#elif IOS || MACCATALYST
//                MauiUIApplicationDelegate.Current.Services;
//#endif
//    }
}
