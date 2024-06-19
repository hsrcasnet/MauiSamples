using Microsoft.AppCenter.Crashes;

namespace MonitoringDemo.Services
{
    [Obsolete("Visual Studio App Center is scheduled for retirement on March 31, 2025.")]
    public interface IAppCenterAnalytics
    {
        void TrackEvent(string name, IDictionary<string, string> properties = null);

        void TrackError(Exception exception, IDictionary<string, string> properties = null, params ErrorAttachmentLog[] attachments);
    }
}