using System.Globalization;

namespace LocalizationDemo.Converters
{
    public class DecimalToStringConverter : IValueConverter
    {
        public string Format { get; set; } = "N";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal decimalValue)
            {
                return decimalValue.ToString(this.Format, Thread.CurrentThread.CurrentCulture);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
