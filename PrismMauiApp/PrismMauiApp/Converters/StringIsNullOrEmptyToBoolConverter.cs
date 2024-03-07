using System.Globalization;

namespace PrismMauiApp.Converters
{
    public class StringIsNullOrEmptyToBoolConverter : BindableObject, IValueConverter
    {
        public static readonly BindableProperty IsInvertedProperty = BindableProperty.Create(
            nameof(IsInverted),
            typeof(bool),
            typeof(StringIsNullOrEmptyToBoolConverter),
            false);

        public bool IsInverted
        {
            get => (bool)this.GetValue(IsInvertedProperty);
            set => this.SetValue(IsInvertedProperty, value);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (this.IsInverted)
            {
                return !string.IsNullOrEmpty(value as string);
            }

            return string.IsNullOrEmpty(value as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}