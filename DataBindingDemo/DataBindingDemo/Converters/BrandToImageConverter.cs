using System.Globalization;

namespace DataBindingDemo.Converters
{
    public class BrandToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string brand)
            {
                if (brand.Contains("ferrari", StringComparison.InvariantCultureIgnoreCase))
                {
                    return "ferrari.png";
                }
                else if (brand.Contains("lamborghini", StringComparison.InvariantCultureIgnoreCase))
                {
                    return "lamborghini.png";
                }
                else if (brand.Contains("mclaren", StringComparison.InvariantCultureIgnoreCase))
                {
                    return "mclaren.png";
                }
            }

            return "dotnet_bot.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
