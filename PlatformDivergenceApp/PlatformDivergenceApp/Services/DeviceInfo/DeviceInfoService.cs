#if ANDROID
using Android.OS;
using Android.Content.PM;
using Application = Android.App.Application;
#elif IOS
using Foundation;
using UIKit;
#elif WINDOWS
using Windows.Security.ExchangeActiveSyncProvisioning;
#endif

namespace PlatformDivergenceApp.Services.DeviceInfo;

public class DeviceInfoService : IDeviceInfoService
{
#if WINDOWS
    private readonly EasClientDeviceInformation deviceInfo = new EasClientDeviceInformation();
#endif

    public string Model
    {
        get
        {
#if ANDROID
            return Build.Model;
#elif IOS
            return UIDevice.CurrentDevice.Model;
#elif WINDOWS
            return this.deviceInfo.SystemProductName;
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
#elif WINDOWS
            return "N/A";
#endif
        }
    }

    public string AppVersion
    {
        get
        {
#if ANDROID
            using (var packageInfo = Application.Context.PackageManager.GetPackageInfo(Application.Context.PackageName, PackageInfoFlags.MetaData))
            {
                return $"{packageInfo.VersionName}";
            }
#elif IOS
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();
#elif WINDOWS
            return "N/A";
#endif
        }
    }
}