using System.Globalization;

namespace DataBindingDemo.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        /// <summary>
        /// Any string format which applies to DateTime values.
        /// https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings
        /// </summary>
        public string Format { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime && !string.IsNullOrEmpty(this.Format))
            {
                return dateTime.ToString(this.Format);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}