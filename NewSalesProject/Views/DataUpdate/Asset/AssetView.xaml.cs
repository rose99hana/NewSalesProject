using NewSalesProject.Interfaces;
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
    /// Interaction logic for AssetView.xaml
    /// </summary>
    public partial class AssetView : UserControl
    {
        public AssetView()
        {
            InitializeComponent();

            DtGrid.AddButton.Click += AddButton_Click;
            DtGrid.DtGrid.SelectionChanged += DtGrid_SelectionChanged;

            HeaderBar.OneView.Checked += OneView_Checked;
            HeaderBar.TwoView.Checked += TwoView_Checked;

            DetailsButton1.IsChecked = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DetailsButton1.IsChecked = true;
        }

        private void TwoView_Checked(object sender, RoutedEventArgs e)
        {
            Grid.SetColumnSpan(View1, 1);
            View2.Visibility = Visibility.Visible;

            DetailsButton2.IsChecked = true; //create view2
        }

        private void OneView_Checked(object sender, RoutedEventArgs e)
        {
            Grid.SetColumnSpan(View1, 3);
            View2.Visibility = Visibility.Collapsed;
        }

        private void NewToggleRadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                DtGrid.AddButton.IsEnabled = true;
                NewButton1.Visibility = Visibility.Collapsed;
                (DataContext as ICRUDViewModel).UpdateItemDetails();
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            DtGrid.AddButton.IsEnabled = false;
            NewButton1.IsChecked = true;
            NewButton1.Visibility = Visibility.Visible;
            (ViewsContent1.Content as NewView).CancelButton_Clicked += DtGrid_SelectionChanged;
        }

        private void DtGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (NewButton1.IsChecked == true)
            {
                (ViewsContent1.Content as NewView).CancelButton_Clicked -= DtGrid_SelectionChanged;
                DetailsButton1.IsChecked = true;
            }

            var temp = ViewsContent1.Content as EditView;
            if (temp != null)
            {
                if (temp.IsReadOnly == false)
                    temp.CancelButton_Click(new object(), new RoutedEventArgs());
            }
        }

        private void MenuToggleRadioButton1_Checked(object sender, RoutedEventArgs e)
        {
            if (ViewsContent1 == null) return;
            var temp = (sender as RadioButton).Name;
            switch (temp)
            {
                case "DetailsButton1":
                    CreateDetailView(ViewsContent1);
                    break;
                case "NewButton1":
                    CreateNewView(ViewsContent1);
                    break;
                case "OrdersButton1":
                    CreateOrderView(ViewsContent1);
                    break;
            }
        }

        private void MenuToggleRadioButton2_Checked(object sender, RoutedEventArgs e)
        {
            if (ViewsContent2 == null) return;
            var temp = (sender as RadioButton).Name;
            switch (temp)
            {
                case "DetailsButton2":
                    CreateDetailView(ViewsContent2);
                    break;
                case "OrdersButton2":
                    CreateOrderView(ViewsContent2);
                    break;
            }
        }

        private void CreateDetailView(ContentControl viewContent)
        {
            EditView editView = new EditView();
            Binding myBinding = new Binding();
            myBinding.Path = new PropertyPath("InEditItem");
            BindingOperations.SetBinding(editView, EditView.BindingItemProperty, myBinding);
            editView.Content = new AssetDetailsContent();
            viewContent.Content = editView;
        }

        private void CreateNewView(ContentControl viewContent)
        {
            NewView newView = new NewView();
            Binding myBinding = new Binding();
            myBinding.Path = new PropertyPath("NewItem");
            BindingOperations.SetBinding(newView, NewView.BindingItemProperty, myBinding);
            newView.Content = new AssetDetailsContent();
            viewContent.Content = newView;
        }

        private void CreateOrderView(ContentControl viewContent)
        {
            viewContent.Content = null;
        }
    }
}
