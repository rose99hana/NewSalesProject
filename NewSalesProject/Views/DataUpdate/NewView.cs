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
    public class NewView : UserControl
    {
        public Button CancelButton;

        static NewView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NewView), new FrameworkPropertyMetadata(typeof(NewView)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            CancelButton = this.Template.FindName("CancelButton", this) as Button;
            CancelButton.Click += CancelButton_Click;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            CancelButton_Clicked.Invoke(sender, e);
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(NewView), new PropertyMetadata("NEW"));

        public object BindingItem
        {
            get { return (object)GetValue(BindingItemProperty); }
            set { SetValue(BindingItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BindingItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BindingItemProperty =
            DependencyProperty.Register("BindingItem", typeof(object), typeof(NewView), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public bool IsAdditionalDataVisible
        {
            get { return (bool)GetValue(IsAdditionalDataVisibleProperty); }
            set { SetValue(IsAdditionalDataVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAdditionalDataVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAdditionalDataVisibleProperty =
            DependencyProperty.Register("IsAdditionalDataVisible", typeof(bool), typeof(NewView), new PropertyMetadata(false));



        public EventHandler CancelButton_Clicked;

    }
}
