using NewSalesProject.Converters;
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
    /// Interaction logic for ProductView.xaml
    /// </summary>
    public partial class ProductView : UserControl
    {
        public ProductView()
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


        private PriceListView priceListView;
        private ReceiptInvoiceView receiptInvoiceView;


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
                case "PriceListButton1":
                    if (CheckTwoView())
                        if (PriceListButton2.IsChecked == true)
                            DetailsButton2.IsChecked = true;
                    CreatePriceListView(ViewsContent1);
                    break;
                case "ReceiptInvoiceButton1":
                    if (CheckTwoView())
                        if (ReceiptInvoiceButton2.IsChecked == true)
                            DetailsButton2.IsChecked = true;
                    CreateReceiptInvoiceView(ViewsContent1);
                    break;
                case "ReceiptsListButton1":
                    CreateReceiptListView(ViewsContent1);
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
                case "PriceListButton2":
                    if (CheckTwoView())
                        if (PriceListButton1.IsChecked == true)
                            DetailsButton1.IsChecked = true;
                    CreatePriceListView(ViewsContent2);
                    break;
                case "ReceiptInvoiceButton2":
                    if (CheckTwoView())
                        if (ReceiptInvoiceButton1.IsChecked == true)
                            DetailsButton1.IsChecked = true;
                    CreateReceiptInvoiceView(ViewsContent2);
                    break;
            }
        }

        private bool CheckTwoView()
        {
            if (HeaderBar.TwoView.IsChecked == true)
                return true;
            return false;
        }

        private void CreateDetailView(ContentControl viewContent)
        {
            EditView editView = new EditView();

            Binding myBinding = new Binding();
            myBinding.Path = new PropertyPath("InEditItem");
            BindingOperations.SetBinding(editView, EditView.BindingItemProperty, myBinding);

            Binding myBinding2 = new Binding();
            myBinding2.ElementName = "DtGrid";
            myBinding2.Path = new PropertyPath("SelectedItem");
            myBinding2.Converter = NullToBooleanConverter.Instance;
            BindingOperations.SetBinding(editView, EditView.IsAdditionalDataVisibleProperty, myBinding2);

            editView.Content = new ProductDetailContent();

            viewContent.Content = editView;
        }

        private void CreateNewView(ContentControl viewContent)
        {
            NewView newView = new NewView();
            Binding myBinding = new Binding();
            myBinding.Path = new PropertyPath("NewItem");
            BindingOperations.SetBinding(newView, NewView.BindingItemProperty, myBinding);
            newView.Content = new ProductDetailContent();
            viewContent.Content = newView;
        }

        private void CreatePriceListView(ContentControl viewContent)
        {
            if(priceListView == null)
            {
                priceListView = new PriceListView();
            }
            viewContent.Content = priceListView;
        }

        private void CreateReceiptInvoiceView(ContentControl viewContent)
        {
            if (receiptInvoiceView == null)
            {
                receiptInvoiceView = new ReceiptInvoiceView();
                receiptInvoiceView.CancelButton.Click += InvoiceCancelButton_Click;
            }
            viewContent.Content = receiptInvoiceView;
        }

        private void CreateReceiptListView(ContentControl viewContent)
        {
            viewContent.Content = null;
        }




        private void InvoiceCancelButton_Click(object sender, RoutedEventArgs e)
        {
            AddInvoiceButton1.Visibility = Visibility.Visible;
            AddInvoiceButton2.Visibility = Visibility.Visible;
            ReceiptInvoiceButton1.Visibility = Visibility.Collapsed;
            ReceiptInvoiceButton2.Visibility = Visibility.Collapsed;
            if (ReceiptInvoiceButton1.IsChecked == true)
                DetailsButton1.IsChecked = true;
            else if (ReceiptInvoiceButton2.IsChecked == true)
                DetailsButton2.IsChecked = true;
        }

        private void AddInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            AddInvoiceButton1.Visibility = Visibility.Collapsed;
            AddInvoiceButton2.Visibility = Visibility.Collapsed;
            ReceiptInvoiceButton1.Visibility = Visibility.Visible;
            ReceiptInvoiceButton2.Visibility = Visibility.Visible;
            var btn = sender as Button;
            if (btn.Name == "AddInvoiceButton1")
                ReceiptInvoiceButton1.IsChecked = true;
            else ReceiptInvoiceButton2.IsChecked = true;
        }

        private void Test_Unchecked(object sender, RoutedEventArgs e)
        {
            var x = e.Source;
        }
    }
}
