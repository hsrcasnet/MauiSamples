#if ANDROID
using Android.OS;
using Android.Content.PM;
using Application = Android.App.Application;
#elif IOS
using Foundation;
using UIKit;
#endif

namespace PlatformDivergenceApp.Services.DeviceInfo
{
    public class DeviceInfoService : IDeviceInfoService
    {
        public string Model
        {
            get
            {
#if ANDROID
                return Build.Model;
#elif IOS
                return UIDevice.CurrentDevice.Model;
#endif
            }
        }

        public string OSVersion
        {
            get
            {
#if ANDROID
                return Build.VERSION.Release;
#elif IOS
                return UIDevice.CurrentDevice.SystemVersion;
#endif
            }
        }

        public string AppVersion
        {
            get
            {
#if ANDROID
                using (var info = Application.Context.PackageManager.GetPackageInfo(Application.Context.PackageName, PackageInfoFlags.MetaData))
                {
                    return $"{info.VersionName}";
                }
#elif IOS
                return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();
#endif
            }
        }
    }
}
