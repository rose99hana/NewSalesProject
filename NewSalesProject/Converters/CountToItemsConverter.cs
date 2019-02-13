using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NewSalesProject.Converters
{
    public class CountToItemsConverter : IValueConverter
    {
        public static readonly CountToItemsConverter Instance = new CountToItemsConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value <= 1)
            {
                return value + " Item";
            }
            else
            {
                return value + " Items";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
