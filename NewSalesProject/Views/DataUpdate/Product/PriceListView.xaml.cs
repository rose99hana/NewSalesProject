using NewSalesProject.Model;
using System;
using System.Collections;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NewSalesProject.Views
{
    /// <summary>
    /// Interaction logic for PriceListView.xaml
    /// </summary>
    public partial class PriceListView : UserControl
    {
        public PriceListView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            btn.Command.Execute(btn.CommandParameter);
            AddPriceWindow priceWindow = new AddPriceWindow(DataContext);
            priceWindow.ShowDialog();
            e.Handled = true;
        }


        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(PriceListView), new PropertyMetadata(null));

        private void ProductPriceDtGrid_Selected(object sender, RoutedEventArgs e)
        {
            if(DetailsBar.Visibility == Visibility.Collapsed)
            {
                Storyboard sb = FindResource("DetailsBarSlideIn") as Storyboard;
                sb.Begin();
            }
        }

        private void ProductPriceDtGrid_Unselected(object sender, RoutedEventArgs e)
        {
            if (DetailsBar.Visibility == Visibility.Visible)
            {
                Storyboard sb = FindResource("DetailsBarSlideOut") as Storyboard;
                sb.Begin();
            }
        }

        private void ProductPriceDtGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ProductPriceDtGrid.SelectedItem == null)
            {
                if (DetailsBar.Visibility == Visibility.Visible)
                {
                    Storyboard sb = FindResource("DetailsBarSlideOut") as Storyboard;
                    sb.Begin();
                }
            }
            else
            {
                if (DetailsBar.Visibility == Visibility.Collapsed)
                {
                    Storyboard sb = FindResource("DetailsBarSlideIn") as Storyboard;
                    sb.Begin();
                }

                (DataContext as ProductViewModel).ProductPriceVM.CancelEditFromView();
            }
        }

        private void VisibleDetailCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            ProductPriceDtGrid.RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode.Visible;
        }

        private void VisibleDetailCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            ProductPriceDtGrid.RowDetailsVisibilityMode = DataGridRowDetailsVisibilityMode.VisibleWhenSelected;
        }
    }
}
