using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace CodeForge.Converters
{
    public class BoolToVisibleConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
                return boolValue; // Avalonia handles true/false visibility directly
            return false; // Default: Hide if not a boolean
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}


