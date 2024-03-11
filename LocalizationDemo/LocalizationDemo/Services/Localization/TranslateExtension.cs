namespace LocalizationDemo.Services.Localization
{
    [ContentProperty("Text")]
    public class TranslateExtension : BindableObject, IMarkupExtension<BindingBase>
    {
        private static Lazy<ILocalizer> localizer;
        private static Lazy<ITranslationProvider> translationProvider;

        public static void Init(Func<ILocalizer> localizerFunction, Func<ITranslationProvider> translationProviderFunction)
        {
            localizer = new Lazy<ILocalizer>(localizerFunction, LazyThreadSafetyMode.PublicationOnly);
            translationProvider = new Lazy<ITranslationProvider>(translationProviderFunction, LazyThreadSafetyMode.PublicationOnly);
        }

        public string Text { get; set; }

        public BindingBase ProvideValue(IServiceProvider serviceProvider)
        {
            var binding = new Binding(nameof(TranslationData.Value))
            {
                Source = new TranslationData(this.Text, localizer.Value, translationProvider.Value),
            };

            return binding;
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return this.ProvideValue(serviceProvider);
        }
    }
}
