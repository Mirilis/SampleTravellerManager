using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace SampleTravelerManager.Converters
{
    public class BoolToVisibilityConverter : MarkupExtension, IValueConverter
    {
        #region Private Fields

        private static BoolToVisibilityConverter instance;

        #endregion Private Fields

        #region Public Methods

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

        #endregion Public Methods
    }
}