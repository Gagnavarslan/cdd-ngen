using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CoreData.Desktop.UI.Converters
{
    public class ResourceImageConverter : IValueConverter
    {
        const int DefaultImageSize = 16;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!int.TryParse((string)parameter, out var size))
            {
                size = DefaultImageSize;
            }

            return value is Uri ? GetImage((Uri)value, size) : GetImage((string)value, size);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotSupportedException();

        private Image GetImage(string resource, int size) =>
            GetImage(new Uri($"pack://application:,,,{resource}", UriKind.Absolute), size);
        private Image GetImage(Uri resource, int size)
        {
            return new Image
            {
                Source = new BitmapImage(resource) { CacheOption = BitmapCacheOption.OnLoad },
                Height = size,
                Width = size,
                Stretch = Stretch.Uniform
            };
        }
    }
}
