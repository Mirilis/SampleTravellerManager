using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows;

namespace SampleTravellerManager.Converters
{
    public class BoolToVisibilityConverter : MarkupExtension, IValueConverter
    {
        private static BoolToVisibilityConverter instance;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value as Boolean?;
            var p = parameter as string;
            if (v != null)
            {
                if (p != null)
                {
                    if (p == "-1")
                    {
                        v = !v;
                    }
                }
                if (v.Value == true)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Hidden;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return instance ?? (instance = new BoolToVisibilityConverter());
        }
    }
}
