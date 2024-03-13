using System.Resources;

namespace LocalizationDemo.Services.Localization
{
    /// <summary>
    /// RESX-based implementation of an ITranslationProvider.
    /// </summary>
    public class ResxTranslationProvider : ITranslationProvider
    {
        private static Lazy<ILocalizer> localizer;
        private static ResourceManager resourceManager;
        private static ResxTranslationProvider instance;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResxTranslationProvider" /> class.
        /// </summary>
        public static ITranslationProvider Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new ResxTranslationProvider();
                }
                return instance;
            }
        }

        private ResxTranslationProvider()
        {
        }

        public static void Init(ResourceManager resourceManager, Func<ILocalizer> localizerFunction)
        {
            localizer = new Lazy<ILocalizer>(localizerFunction, LazyThreadSafetyMode.PublicationOnly);
            ResxTranslationProvider.resourceManager = resourceManager;
        }

        /// <summary>
        ///     See <see cref="ITranslationProvider.Translate" />
        /// </summary>
        public string Translate(string key)
        {
            var currentCulture = localizer.Value.GetCurrentCulture();
            var translatedValue = resourceManager.GetString(key, currentCulture);
            if (translatedValue != null)
            {
                return translatedValue;
            }

            return $"#{key}#";
        }
    }
}