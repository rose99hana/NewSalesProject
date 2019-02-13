using NewSalesProject.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace NewSalesProject.Behavior
{
    public static class TextBlockFormatChangeBehavior
    {


        public static string GetCurrencySymbol(DependencyObject obj)
        {
            return (string)obj.GetValue(CurrencySymbolProperty);
        }

        public static void SetCurrencySymbol(DependencyObject obj, string value)
        {
            obj.SetValue(CurrencySymbolProperty, value);
        }

        // Using a DependencyProperty as the backing store for CurrencySymbol.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrencySymbolProperty =
            DependencyProperty.RegisterAttached("CurrencySymbol", typeof(string), typeof(TextBlockFormatChangeBehavior), new UIPropertyMetadata("$", CurrencySymbolChanged));

        private static void CurrencySymbolChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            dynamic temp = "";
            var oldBinding = new Binding();
            var newBinding = new Binding();
            if (d is TextBlock)
            {
                temp = d as TextBlock;
                oldBinding = temp.GetBindingExpression(TextBlock.TextProperty).ParentBinding;
            }
            //else if (d is Run)
            //{
            //    temp = d as Run;
            //    oldBinding = temp.GetBindingExpression(Run.TextProperty).ParentBinding;
            //}
            //else if (d is NumberTextbox)
            //{
            //    temp = d as NumberTextbox;
            //    var x = temp.GetBindingExpression(NumberTextbox.TextProperty);
            //    if (x == null) return;
            //    oldBinding = x.ParentBinding;
            //}
            switch (e.NewValue)
            {
                case "₫":
                    CloneBinding(oldBinding, newBinding, App.VietNamCurrency);
                    newBinding.StringFormat = "{0:C0}";
                    break;
                case "$":
                    CloneBinding(oldBinding, newBinding, App.USCurrency);
                    newBinding.StringFormat = "{0:C}";
                    break;
                case "¥":
                    CloneBinding(oldBinding, newBinding, App.JapanCurrency);
                    newBinding.StringFormat = "{0:C0}";
                    break;
                case "€":
                    CloneBinding(oldBinding, newBinding, App.GermanyCurrency);
                    newBinding.StringFormat = "{0:C}";
                    break;
                default:
                    break;
            }
            if (temp is TextBlock)
            {
                temp.SetBinding(TextBlock.TextProperty, newBinding);
            }
            //if (temp is Run)
            //{
            //    temp.SetBinding(Run.TextProperty, newBinding);
            //}
            //else if (d is NumberTextbox)
            //{
            //    temp.SetBinding(NumberTextbox.TextProperty, newBinding);
            //}
        }

        private static void CloneBinding(Binding oldBinding, Binding newBinding, CultureInfo cultureInfo)
        {
            newBinding.Path = oldBinding.Path;
            newBinding.ElementName = oldBinding.ElementName;
            newBinding.StringFormat = oldBinding.StringFormat;
            newBinding.ConverterCulture = cultureInfo;
        }
    }
}
