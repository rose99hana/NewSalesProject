using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NewSalesProject.Converters
{
    public class CurrencyToSymbolConverter : IValueConverter
    {
        public static readonly CurrencyToSymbolConverter Instance = new CurrencyToSymbolConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "";
            switch (value.ToString())
            {
                case "₫":
                    return "VND(₫)";
                case "$":
                    return "USD($)";
                case "¥":
                    return "JPY(¥)";
                case "€":
                    return "EUR(€)";
                default:
                    return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value.ToString())
            {
                case "VND(₫)":
                    return "₫";
                case "USD($)":
                    return "$";
                case "JPY(¥)":
                    return "¥";
                case "EUR(€)":
                    return "€";
                default:
                    return "";
            }
        }
    }
}
