using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace AppCenterDemo.Services
{
    public class AppCenterAnalytics : IAnalytics
    {
        public AppCenterAnalytics()
        {
            AppCenter.Start(
               $"ios=ad52a6d1-2e81-4af1-9398-54e53603311e;" +
               $"android=c45fb280-e432-44cb-8b1c-eefa1c63fbd4;" +
               typeof(Analytics),
               typeof(Crashes));
        }

        public void TrackEvent(string name, IDictionary<string, string> properties = null)
        {
            Analytics.TrackEvent(name, properties);
        }

        public void TrackError(Exception exception, IDictionary<string, string> properties = null, params ErrorAttachmentLog[] attachments)
        {
            Crashes.TrackError(exception, properties, attachments);
        }
    }
}
