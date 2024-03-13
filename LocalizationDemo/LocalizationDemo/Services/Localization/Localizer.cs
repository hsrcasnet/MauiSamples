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
            var hasChanged = cultureInfo != Thread.CurrentThread.CurrentUICulture;

            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;


            if (hasChanged)
            {
                this.OnLocaleChanged(cultureInfo);
            }
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