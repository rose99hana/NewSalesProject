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
    /// Interaction logic for WishListView.xaml
    /// </summary>
    public partial class WishListView : UserControl
    {
        public WishListView()
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

        private void WishListDtGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext != null)
                (DataContext as ProductViewModel).WishListVM.WishListDetailVM.CancelEditFromView();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            btn.Command.Execute(btn.CommandParameter);
        }

        private void WishListDtGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var dtGrid = sender as DataGrid;
            if(dtGrid.Columns[0].Width.IsStar == false)
                dtGrid.Columns[0].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
    }
}
