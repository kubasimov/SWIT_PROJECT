using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPF.Converter
{
    public class BooleanToVisibilityConverter:IValueConverter

    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (true.Equals(value)) ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}