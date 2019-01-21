using CoreData.Common.ModelNotifyChanged;
using System.Windows;
using System.Windows.Controls;

namespace CoreData.Desktop.Common.Windows
{
    public class TabItemData : ViewModel
    {
        public string Title
        {
            get => Properties.Get(default(string));
            set => Properties.Set(value);
        }

        public string Image
        {
            get => Properties.Get(default(string));
            set => Properties.Set(value);
        }

        public object Content
        {
            get => Properties.Get(default(object));
            set => Properties.Set(value);
        }
    }

    public class ImageTabItem : TabItem
    {
        public ImageTabItem()
        {
            Loaded += TabItemLoaded;
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(ImageTabItem), new PropertyMetadata(string.Empty));

        public string ImageSource
        {
            get => (string)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register(nameof(ImageSource), typeof(string), typeof(ImageTabItem), new PropertyMetadata(string.Empty));

        private void TabItemLoaded(object sender, RoutedEventArgs e)
        { // https://docs.catelproject.com/vnext/tips-tricks/mvvm/using-tabbed-interface-with-mvvm/#code-behind
        }
    }
}
