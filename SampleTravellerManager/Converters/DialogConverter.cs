using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace SampleTravellerManager.Converters
{
    public class DialogConverter : MarkupExtension, IValueConverter
    {
        private static DialogConverter instance;
   

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var par = parameter as string;
            var val = value as DialogType?;
            if (par == "List")
            {
                

                if (val.Value.HasFlag(DialogType.Traveller))
                {
                    return new Controls.TravellersListControl();
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
    }
}
