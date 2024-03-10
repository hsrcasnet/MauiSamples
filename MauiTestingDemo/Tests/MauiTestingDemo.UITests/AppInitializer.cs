using Xamarin.UITest;
using Platform = Xamarin.UITest.Platform;

namespace MauiTestingDemo.UITests
{
    internal static class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            switch (platform)
            {
                case Platform.Android:
                    return ConfigureApp
                                .Android
                                .ApkFile("../../../../../MauiTestingDemo/bin/Debug/net8.0-android/com.companyname.MauiTestingDemo.apk")
                                .StartApp(Xamarin.UITest.Configuration.AppDataMode.Clear);

                case Platform.iOS:
                    return ConfigureApp
                                .iOS
                                //.InstalledApp("com.companyname.MauiTestingDemo")
                                .AppBundle("../../../../../MauiTestingDemo/bin/Debug/net8.0-ios/iossimulator-x64/MauiTestingDemo.app")
                                .StartApp(Xamarin.UITest.Configuration.AppDataMode.Clear);

                default:
                    throw new NotSupportedException();
            };
        }
    }
}