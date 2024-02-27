using System.Globalization;

namespace DataBindingDemo.Converters
{
    public class BoolToValueConverter<T> : BindableObject, IValueConverter
    {
        public static readonly BindableProperty TrueValueProperty =
            BindableProperty.Create(
                nameof(TrueValue),
                typeof(T),
                typeof(BoolToValueConverter<T>));

        public T TrueValue
        {
            get => (T)this.GetValue(TrueValueProperty);
            set => this.SetValue(TrueValueProperty, value);
        }

        public static readonly BindableProperty FalseValueProperty =
            BindableProperty.Create(
                nameof(FalseValue),
                typeof(T),
                typeof(BoolToValueConverter<T>));

        public T FalseValue
        {
            get => (T)this.GetValue(FalseValueProperty);
            set => this.SetValue(FalseValueProperty, value);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b && b)
            {
                return this.TrueValue;
            }

            return this.FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}