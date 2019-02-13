using NewSalesProject.Supports;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NewSalesProject.Converters
{
    public class MultiBindingToCommandParaObjectConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            CommandParaObject commandObject = new CommandParaObject();
            commandObject.CommandName = (string)values[0];
            commandObject.CommandParameter = values[1];
            return commandObject;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
