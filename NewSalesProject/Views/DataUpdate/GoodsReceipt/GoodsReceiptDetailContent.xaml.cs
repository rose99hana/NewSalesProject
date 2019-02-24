using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for ReceiptInvoiceView.xaml
    /// </summary>
    public partial class GoodsReceiptDetailContent : UserControl
    {
        public GoodsReceiptDetailContent()
        {
            InitializeComponent();
        }

        private void OpenDialogButton_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.OpenDialogCommand.Execute(null, sender as Button);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, sender as Button);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            btn.Command.Execute(btn.CommandParameter);
        }
    }
}
