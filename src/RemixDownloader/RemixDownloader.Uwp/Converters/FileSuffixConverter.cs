using System;
using Windows.UI.Xaml.Data;

namespace RemixDownloader.Uwp.Converters
{
    public class FileSuffixConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is long fileSizeInBytes)
            {
                if (fileSizeInBytes < 0)
                {
                    return "0";
                }

                if (fileSizeInBytes == 0)
                {
                    return $"{fileSizeInBytes:N2}";
                }

                var mag = (int)Math.Log(fileSizeInBytes, 1024);

                var adjustedSize = (decimal)fileSizeInBytes / (1L << (mag * 10));

                if (Math.Round(adjustedSize, 2) >= 1000)
                {
                    mag += 1;
                    adjustedSize /= 1024;
                }

                var suffixes = new[] { "bytes", "KB", "MB", "GB", "TB" };

                return $"{adjustedSize:N2} {suffixes[mag]}";
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
