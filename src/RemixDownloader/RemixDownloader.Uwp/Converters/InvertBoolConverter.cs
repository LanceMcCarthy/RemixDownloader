using System;
using Windows.UI.Xaml.Data;

namespace RemixDownloader.Uwp.Converters
{
    public class InvertBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value is bool val)
            {
                return !val;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is bool val)
            {
                return !val;
            }

            return value;
        }
    }
}
