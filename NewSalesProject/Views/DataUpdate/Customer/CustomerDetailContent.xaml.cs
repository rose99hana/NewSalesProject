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

namespace NewSalesProject.Views
{
    /// <summary>
    /// Interaction logic for CustomerDetailView.xaml
    /// </summary>
    public partial class CustomerDetailContent : UserControl
    {
        public CustomerDetailContent()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.Parent is NewView)
            {
                var uc = (NewView)this.Parent;
                Keyboard.Focus(NameTextBox);
            }
        }

        //public bool AutoFocus
        //{
        //    get { return (bool)GetValue(AutoFocusProperty); }
        //    set { SetValue(AutoFocusProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for AutoFocus.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty AutoFocusProperty =
        //    DependencyProperty.Register("AutoFocus", typeof(bool), typeof(CustomerDetailContent), new PropertyMetadata(false));




        //public bool IsReadOnly
        //{
        //    get { return (bool)GetValue(IsReadOnlyProperty); }
        //    set { SetValue(IsReadOnlyProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for IsReadOnly.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty IsReadOnlyProperty =
        //    DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(CustomerDetailContent), new PropertyMetadata(true));




        //public bool IsControlsEnable
        //{
        //    get { return (bool)GetValue(IsControlsEnableProperty); }
        //    set { SetValue(IsControlsEnableProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for IsControlsEnable.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty IsControlsEnableProperty =
        //    DependencyProperty.Register("IsControlsEnable", typeof(bool), typeof(CustomerDetailContent), new PropertyMetadata(false));

    }
}
