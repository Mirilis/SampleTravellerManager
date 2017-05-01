using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace SampleTravellerManager.Converters
{
    public class BoolInverter : MarkupExtension, IValueConverter
    {
        private static BoolInverter instance;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (value != null)
            {
                var p = value as Boolean?;
                return !p.Value;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var p = value as Boolean?;
                return !p.Value;
            }
            return null;
        }


        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return instance ?? (instance = new BoolInverter());
        }
    }

}
