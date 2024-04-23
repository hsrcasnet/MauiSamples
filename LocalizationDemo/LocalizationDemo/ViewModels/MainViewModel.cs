using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using LocalizationDemo.Services.Localization;
using LocalizationDemo.Resources.Text;

namespace LocalizationDemo.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly ILocalizer localizer;
        private readonly ITranslationProvider translationProvider;

        private CultureInfo selectedLanguage;
        private decimal number;
        private DateTime dateTime;

        public MainViewModel(
            ILocalizer localizer,
            ITranslationProvider translationProvider)
        {
            this.localizer = localizer;
            this.translationProvider = translationProvider;

            this.selectedLanguage = this.localizer.GetCurrentCulture();
            this.OnPropertyChanged(nameof(this.SelectedLanguage));

            this.Number = 1234567.89m;
            this.DateTime = DateTime.Now;
        }

        public CultureInfo SelectedLanguage
        {
            get => this.selectedLanguage;
            set
            {
                if (this.SetProperty(ref this.selectedLanguage, value))
                {
                    this.localizer.SetCultureInfo(value);

                    this.OnPropertyChanged(nameof(this.SelectedLanguage));
                    this.OnPropertyChanged(nameof(this.CurrentCulture));
                    this.OnPropertyChanged(nameof(this.CurrentUICulture));
                    this.OnPropertyChanged(nameof(this.DefaultThreadCurrentCulture));
                    this.OnPropertyChanged(nameof(this.DefaultThreadCurrentUICulture));
                    this.OnPropertyChanged(nameof(this.HelloWorldLabelText));
                    this.OnPropertyChanged(nameof(this.DateTime));
                    this.OnPropertyChanged(nameof(this.Number));
                }
            }
        }

        public CultureInfo CurrentCulture
        {
            get => Thread.CurrentThread.CurrentCulture;
        }

        public CultureInfo CurrentUICulture
        {
            get => Thread.CurrentThread.CurrentUICulture;
        }

        public CultureInfo DefaultThreadCurrentCulture
        {
            get => CultureInfo.DefaultThreadCurrentCulture;
        }

        public CultureInfo DefaultThreadCurrentUICulture
        {
            get => CultureInfo.DefaultThreadCurrentUICulture;
        }

        public IEnumerable<CultureInfo> Languages => Services.Localization.Languages.GetAll().ToArray();

        public string HelloWorldLabelText
        {
            get => this.translationProvider.Translate(nameof(Strings.HelloWorldLabelText));
        }

        public decimal Number
        {
            get => this.number;
            private set => this.SetProperty(ref this.number, value);
        }

        public DateTime DateTime
        {
            get => this.dateTime;
            private set => this.SetProperty(ref this.dateTime, value);
        }
    }
}
