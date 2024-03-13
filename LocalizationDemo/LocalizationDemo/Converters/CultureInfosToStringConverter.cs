using System.Globalization;

namespace LocalizationDemo.Converters
{
    public class CultureInfosToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<CultureInfo> cultureInfos)
            {
                return string.Join(", ", cultureInfos.Select(c => c.Name));
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
