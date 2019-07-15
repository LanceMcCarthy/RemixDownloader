using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace RemixDownloader.Uwp.Converters
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string text)
            {
                if (string.IsNullOrEmpty(text))
                {
                    return Visibility.Collapsed;
                }

                return Visibility.Visible;
            }

            if (value == null)
            {
                return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
