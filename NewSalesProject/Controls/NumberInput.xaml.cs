using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for NumberInput.xaml
    /// </summary>
    public partial class NumberInput : UserControl
    {
        public NumberInput()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (PopupTextbox.SelectionLength > 0)
            {
                StringBuilder stBuilder = new StringBuilder(PopupTextbox.Text);
                stBuilder.Remove(PopupTextbox.SelectionStart, PopupTextbox.SelectionLength);
                Text = stBuilder.ToString();
            }
            var temp = PopupTextbox.SelectionStart + 1;
            var x = e.OriginalSource;
            var num = (sender as Button).Content.ToString();
            if (num == "00")
            {
                Text += "00";
                temp += 1;
            }
            else if (num == "000")
            {
                Text += "000";
                temp += 2;
            }
            else Text += (sender as Button).Content;

            PopupTextbox.Focus();
            PopupTextbox.Select(temp, 0);
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(PopupTextbox.Text))
            {
                return;
            }
            else
            {
                if (PopupTextbox.SelectionLength > 0)
                {
                    Text = PopupTextbox.Text.Remove(PopupTextbox.SelectionStart, PopupTextbox.SelectionLength);
                }
                else
                {
                    var z = PopupTextbox.SelectionStart;
                    if (z > 0)
                    {
                        var x = PopupTextbox.Text.Substring(0, z - 1);
                        var y = PopupTextbox.Text.Substring(z, PopupTextbox.Text.Length - z);
                        Text = x + y;
                    }
                    else
                    {
                        Text = PopupTextbox.Text.Substring(0, PopupTextbox.Text.Length - 1);
                    }
                    PopupTextbox.Focus();
                    PopupTextbox.Select(z - 1, 0);
                }
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Text = "";
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(NumberInput), new PropertyMetadata(""));

        //private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)     //To prevent this event swallow click event
        //{
        //    if (PopupTextbox.SelectionLength > 0)
        //    {
        //        StringBuilder stBuilder = new StringBuilder(PopupTextbox.Text);
        //        stBuilder.Remove(PopupTextbox.SelectionStart, PopupTextbox.SelectionLength);
        //        Text = stBuilder.ToString();
        //    }
        //    var temp = PopupTextbox.SelectionStart + 1;
        //    var x = e.OriginalSource;
        //    var num = (sender as Button).Content.ToString();
        //    if (num == "00")
        //    {
        //        Text += "00";
        //        temp += 1;
        //    }
        //    else if (num == "000")
        //    {
        //        Text += "000";
        //        temp += 2;
        //    }
        //    else Text += (sender as Button).Content;

        //    PopupTextbox.Focus();
        //    PopupTextbox.Select(temp, 0);
        //}

        //private void ClearButton_PreviewMouseDown(object sender, MouseButtonEventArgs e) //To prevent this event swallow click event
        //{
        //    Text = "";
        //}

        //private void Backspace_PreviewMouseDown(object sender, MouseButtonEventArgs e) //To prevent this event swallow click event
        //{
        //    if (String.IsNullOrEmpty(PopupTextbox.Text))
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        if (PopupTextbox.SelectionLength > 0)
        //        {
        //            Text = PopupTextbox.Text.Remove(PopupTextbox.SelectionStart, PopupTextbox.SelectionLength);
        //        }
        //        else
        //        {
        //            var z = PopupTextbox.SelectionStart;
        //            if (z > 0)
        //            {
        //                var x = PopupTextbox.Text.Substring(0, z - 1);
        //                var y = PopupTextbox.Text.Substring(z, PopupTextbox.Text.Length - z);
        //                Text = x + y;
        //            }
        //            else
        //            {
        //                Text = PopupTextbox.Text.Substring(0, PopupTextbox.Text.Length - 1);
        //            }
        //            PopupTextbox.Focus();
        //            PopupTextbox.Select(z - 1, 0);
        //        }
        //    }
        //}
    }
}
