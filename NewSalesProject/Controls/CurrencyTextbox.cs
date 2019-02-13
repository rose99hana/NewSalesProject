using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace NewSalesProject.Controls
{
    public class CurrencyTextbox : TextBox
    {
        public CurrencyTextbox()
        {
            RoutedEventHandler textBox_GotFocus = TextBox_GotFocus;
            AddHandler(CurrencyTextbox.GotFocusEvent, textBox_GotFocus);
        }


        public Decimal OriginText
        {
            get { return (Decimal)GetValue(OriginTextProperty); }
            set { SetValue(OriginTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OriginText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OriginTextProperty =
            DependencyProperty.Register("OriginText", typeof(Decimal), typeof(CurrencyTextbox), new FrameworkPropertyMetadata(0m, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public string Currency
        {
            get { return (string)GetValue(CurrencyProperty); }
            set { SetValue(CurrencyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Currency.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrencyProperty =
            DependencyProperty.Register("Currency", typeof(string), typeof(CurrencyTextbox), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, CurrencyPropertyChangedCallback));

        private static void CurrencyPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            Binding myBinding = new Binding();
            myBinding.Path = new PropertyPath("OriginText");
            myBinding.RelativeSource = RelativeSource.Self;
            switch (d.GetValue(CurrencyProperty))
            {
                case "¥":
                    myBinding.ConverterCulture = App.JapanCurrency;
                    myBinding.StringFormat = "{0:C0}";
                    break;
                case "₫":
                    myBinding.ConverterCulture = App.VietNamCurrency;
                    myBinding.StringFormat = "{0:C0}";
                    break;
                case "€":
                    myBinding.ConverterCulture = App.GermanyCurrency;
                    myBinding.StringFormat = "{0:C}";
                    break;
                case "$":
                    myBinding.ConverterCulture = App.USCurrency;
                    myBinding.StringFormat = "{0:C}";
                    break;
                case "%":
                    myBinding.StringFormat = "{0:N1}%";
                    break;
                case "number":
                    myBinding.StringFormat = "{0:N0}";
                    break;
            }
            //myBinding.StringFormat = "{0:C0}";
            (d as CurrencyTextbox).SetBinding(CurrencyTextbox.TextProperty, myBinding);
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Text = OriginText.ToString();
            SelectAll();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            
        }


        //public string FormatType
        //{
        //    get { return (string)GetValue(FormatTypeProperty); }
        //    set { SetValue(FormatTypeProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for FormatType.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty FormatTypeProperty =
        //    DependencyProperty.Register("FormatType", typeof(string), typeof(CurrencyTextbox), new PropertyMetadata(""));
    }
}
