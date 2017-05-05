using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace SampleTravelerManager.Converters
{
    public class DialogConverter : MarkupExtension, IValueConverter
    {
        #region Private Fields

        private static DialogConverter instance;

        #endregion Private Fields

        #region Public Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var par = parameter as string;
            var val = value as DialogTypes?;
            if (par == "List")
            {
                if (val.Value.HasFlag(DialogTypes.Traveler))
                {
                    return new Controls.TravelersListControl();
                }
                else
                {
                    return new Controls.QuestionsListControl();
                }
            }
            else
            {
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return instance ?? (instance = new DialogConverter());
        }

        #endregion Public Methods
    }
}