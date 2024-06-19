using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace MonitoringDemo.Services
{
    [Obsolete("Visual Studio App Center is scheduled for retirement on March 31, 2025.")]
    public class AppCenterAnalytics : IAppCenterAnalytics
    {
        public AppCenterAnalytics()
        {
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
