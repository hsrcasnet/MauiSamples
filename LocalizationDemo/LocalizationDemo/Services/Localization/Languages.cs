using System.Globalization;

namespace LocalizationDemo.Services.Localization
{
    internal static class Languages
    {
        internal static readonly CultureInfo English = new CultureInfo("en");
        internal static readonly CultureInfo German = new CultureInfo("de");
        internal static readonly CultureInfo GermanSwitzerland = new CultureInfo("de-CH");
        internal static readonly CultureInfo GermanGermany = new CultureInfo("de-DE");
        internal static readonly CultureInfo French = new CultureInfo("fr");
        internal static readonly CultureInfo Italian = new CultureInfo("it");

        internal static IEnumerable<CultureInfo> GetAll()
        {
            yield return English;
            yield return German;
            yield return GermanSwitzerland;
            yield return GermanGermany;
            yield return French;
            yield return Italian;
        }
    }
}
