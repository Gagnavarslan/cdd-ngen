using System.Windows;
using System.Windows.Media;

namespace CoreData.Desktop.Common.Windows
{
    public static class DependencyObjectExtensions
    {
        /// <summary>Returns the first parent of <paramref name="source"/> of type <typeparamref name="T"/></summary>
        public static T VisualTreeGetParentOfType<T>(this DependencyObject source)
            where T : DependencyObject
        {
            if (source != null)
            {
                while ((source = VisualTreeHelper.GetParent(source)) != null)
                {
                    if (source is T) break;
                }
            }
            return (T)source;
        }
    }
}
