using System.Globalization;

namespace LocalizationDemo.Services.Localization
{
    public class Localizer : ILocalizer
    {
        private static Lazy<ILocalizer> Implementation = new Lazy<ILocalizer>(CreateLocalizer, LazyThreadSafetyMode.PublicationOnly);

        private static ILocalizer CreateLocalizer()
        {
            return new Localizer();
        }

        public static ILocalizer Current
        {
            get => Implementation.Value;
        }

        private Localizer()
        {
        }

        public void SetCultureInfo(CultureInfo cultureInfo)
        {
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            this.OnLocaleChanged(cultureInfo);
        }

        public event EventHandler<CultureInfoChangedEventArgs> CultureInfoChangedEvent;

        protected virtual void OnLocaleChanged(CultureInfo cultureInfo)
        {
            this.CultureInfoChangedEvent?.Invoke(this, new CultureInfoChangedEventArgs(cultureInfo));
        }

        public CultureInfo GetCurrentCulture()
        {
            return Thread.CurrentThread.CurrentCulture;
        }
    }
}