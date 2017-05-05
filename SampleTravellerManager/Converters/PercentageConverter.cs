using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace SampleTravelerManager.Converters
{
    public class PercentageConverter : MarkupExtension, IValueConverter
    {
        #region Private Fields

        private static PercentageConverter instance;

        #endregion Private Fields

        #region Public Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return System.Convert.ToDouble(value) * System.Convert.ToDouble(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return instance ?? (instance = new PercentageConverter());
        }

        #endregion Public Methods
    }
}