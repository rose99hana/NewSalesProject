using NewSalesProject.Model;
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
    /// Interaction logic for NumberTextbox.xaml
    /// </summary>
    public partial class NumberTextbox : UserControl
    {
        public NumberTextbox()
        {
            InitializeComponent();
            InputPopup.OkButton.Click += OkButton_Clicked;
        }

        private void OkButton_Clicked(object sender, RoutedEventArgs e)
        {
            IsPopupOpen = false;
            OriginText = InputPopup.Text == "" ? "0" : InputPopup.Text;
            var binding = GetBindingExpression(NumberTextbox.OriginTextProperty);
            if(binding != null)
                binding.UpdateSource();
        }

        public string OriginText
        {
            get { return (string)GetValue(OriginTextProperty); }
            set { SetValue(OriginTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OriginText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OriginTextProperty =
            DependencyProperty.Register("OriginText", typeof(string), typeof(NumberTextbox), new FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public string Currency
        {
            get { return (string)GetValue(CurrencyProperty); }
            set { SetValue(CurrencyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Currency.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrencyProperty =
            DependencyProperty.Register("Currency", typeof(string), typeof(NumberTextbox), new FrameworkPropertyMetadata(""));

        public bool IsPopupOpen
        {
            get { return (bool)GetValue(IsPopupOpenProperty); }
            set { SetValue(IsPopupOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsPopupOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsPopupOpenProperty =
            DependencyProperty.Register("IsPopupOpen", typeof(bool), typeof(NumberTextbox), new PropertyMetadata(false));

        private void button_Click(object sender, RoutedEventArgs e)
        {
            IsPopupOpen = true;
            InputPopup.Text = OriginText.Trim('%', '₫', '$', '¥', '€');
            InputPopup.PopupTextbox.Focus();
        }

        private void popup_Opened(object sender, EventArgs e)
        {
            var x = popup.Placement;
        }
    }
}
