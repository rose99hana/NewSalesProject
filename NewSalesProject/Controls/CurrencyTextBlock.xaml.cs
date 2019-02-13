using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NewSalesProject.Controls
{
    /// <summary>
    /// Interaction logic for CurrencyTextBlock.xaml
    /// </summary>
    public partial class CurrencyTextBlock : UserControl
    {
        public CurrencyTextBlock()
        {
            InitializeComponent();
        }

        public int Text
        {
            get { return (int)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(int), typeof(CurrencyTextBlock), new PropertyMetadata(0));




        public string Currency
        {
            get { return (string)GetValue(CurrencyProperty); }
            set { SetValue(CurrencyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Currency.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrencyProperty =
            DependencyProperty.Register("Currency", typeof(string), typeof(CurrencyTextBlock), new PropertyMetadata("$"));



    }
}
