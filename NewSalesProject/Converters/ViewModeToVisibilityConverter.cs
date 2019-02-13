using NewSalesProject.Enum;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace NewSalesProject.Converters
{
    public class ViewModeToVisibilityConverter : IValueConverter
    {
        public static readonly ViewModeToVisibilityConverter Instance = new ViewModeToVisibilityConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((ViewModeType)value == ViewModeType.Busy)
            {
                return Visibility.Visible;
            }
            else return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
