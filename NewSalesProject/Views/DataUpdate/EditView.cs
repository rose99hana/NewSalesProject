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
    public class EditView : UserControl
    {
        public Button EditButton;
        public Button CancelButton;
        public Button SaveButton;
        public string Alas { get; set; } = "Detail";

        static EditView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EditView), new FrameworkPropertyMetadata(typeof(EditView)));
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            EditButton = this.Template.FindName("EditButton", this) as Button;
            EditButton.Click += EditButton_Click;
            CancelButton = this.Template.FindName("CancelButton", this) as Button;
            CancelButton.Click += CancelButton_Click;
            SaveButton = this.Template.FindName("SaveButton", this) as Button;
            SaveButton.Click += CancelButton_Click;
        }


        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsReadOnly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(EditView), new PropertyMetadata(true));



        public bool IsAdditionalDataVisible
        {
            get { return (bool)GetValue(IsAdditionalDataVisibleProperty); }
            set { SetValue(IsAdditionalDataVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAdditionalDataVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAdditionalDataVisibleProperty =
            DependencyProperty.Register("IsAdditionalDataVisible", typeof(bool), typeof(EditView), new PropertyMetadata(true));



        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(EditView), new PropertyMetadata("DETAILS"));


        public bool IsControlsEnable
        {
            get { return (bool)GetValue(IsControlsEnableProperty); }
            set { SetValue(IsControlsEnableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsControlsEnable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsControlsEnableProperty =
            DependencyProperty.Register("IsControlsEnable", typeof(bool), typeof(EditView), new PropertyMetadata(false));


        public object BindingItem
        {
            get { return (object)GetValue(BindingItemProperty); }
            set { SetValue(BindingItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BindingItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BindingItemProperty =
            DependencyProperty.Register("BindingItem", typeof(object), typeof(EditView), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));



        public virtual void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditButton.Visibility = Visibility.Collapsed;
            CancelButton.Visibility = Visibility.Visible;
            SaveButton.Visibility = Visibility.Visible;
            this.IsReadOnly = false;
            this.IsControlsEnable = true;

        }

        public virtual void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            EditButton.Visibility = Visibility.Visible;
            CancelButton.Visibility = Visibility.Collapsed;
            SaveButton.Visibility = Visibility.Collapsed;
            this.IsReadOnly = true;
            this.IsControlsEnable = false;
        }
    }
}
