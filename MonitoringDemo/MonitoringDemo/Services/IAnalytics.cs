using Microsoft.AppCenter.Crashes;

namespace MonitoringDemo.Services
{
    public interface IAnalytics
    {
        void TrackEvent(string name, IDictionary<string, string> properties = null);

        void TrackError(Exception exception, IDictionary<string, string> properties = null, params ErrorAttachmentLog[] attachments);
    }
}