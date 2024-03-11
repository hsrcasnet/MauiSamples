using Foundation;

namespace MauiTestingDemo
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp()
        {
#if ENABLE_TEST_CLOUD
            Xamarin.Calabash.Start();
#endif

            return MauiProgram.CreateMauiApp();
        }
    }
}
