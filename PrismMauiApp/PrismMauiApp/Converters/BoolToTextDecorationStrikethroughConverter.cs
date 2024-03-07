using System.Globalization;

namespace PrismMauiApp.Converters
{
    public class BoolToTextDecorationStrikethroughConverter : BindableObject, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool strikethrough && strikethrough
                ? TextDecorations.Strikethrough
                : TextDecorations.None;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}