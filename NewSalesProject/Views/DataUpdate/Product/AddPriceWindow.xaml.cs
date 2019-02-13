using NewSalesProject.Model;
using NewSalesProject.Supports;
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
using System.Windows.Shapes;

namespace NewSalesProject.Views
{
    /// <summary>
    /// Interaction logic for AddPriceWindow.xaml
    /// </summary>
    public partial class AddPriceWindow : Window
    {
        public AddPriceWindow()
        {
            InitializeComponent();
        }

        public AddPriceWindow(object dataContext)
        {
            DataContext = dataContext;
            InitializeComponent();

            if (DataAccess.Stores.Count == 0)
                DataAccess.GetAllStores();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            StoreListView.SelectedItem = null;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
        }

    }
}
