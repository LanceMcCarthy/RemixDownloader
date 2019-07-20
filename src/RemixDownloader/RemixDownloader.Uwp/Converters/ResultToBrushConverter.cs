using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace RemixDownloader.Uwp.Converters
{
    public class ResultToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string status)
            {
                var lowerStatus = status.ToLower();

                if (lowerStatus.Contains("downloading") || lowerStatus.Contains("saving"))
                {
                    return new SolidColorBrush(Colors.Goldenrod);
                }

                if (lowerStatus.Contains("saved"))
                {
                    return new SolidColorBrush(Colors.MediumSeaGreen);
                }

                if (lowerStatus.Contains("failed"))
                {
                    return new SolidColorBrush(Colors.DarkRed);
                }

                return new SolidColorBrush(Colors.Gray);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
