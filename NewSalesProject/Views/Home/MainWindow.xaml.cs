using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Menu Slide Effect Field
        ColumnDefinition MenuColumnInMain;

        bool IsSlidingOut = false;
        bool IsMenuPinned = false;
        #endregion

        public MainWindow()
        {
            MainViewModel m = new MainViewModel();
            DataContext = m;
            MenuColumnInMain = new ColumnDefinition();
            MenuColumnInMain.SharedSizeGroup = "MenuPanelPlace";
            InitializeComponent();
        }

        #region Menu Slide Effect

        private void MenuToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsMenuPinned == false)
            {
                IsMenuPinned = true;
                ToggleOn();
            }
            else
            {
                IsMenuPinned = false;
                ToggleOff();
            }
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MenuButton.IsEnabled = false;
            if (MenuPanel.Visibility == Visibility.Collapsed)
            {
                MenuPanel.Focus();
                MenuPanelSlideIn();
            }
            else
            {
                MenuPanelSlideOut();
                if (IsMenuPinned == true)
                {
                    IsMenuPinned = false;
                    ToggleOff();
                }
            }
        }

        protected void MenuPanelSlideIn()
        {
            Storyboard sb = FindResource("MenuSlideIn") as Storyboard;
            sb.Completed += SlidingIn_Completed;
            sb.Begin();
        }

        private void SlidingIn_Completed(object sender, EventArgs e)
        {
            MenuButton.IsEnabled = true;
        }

        protected void MenuPanelSlideOut()
        {
            IsSlidingOut = true;
            Storyboard sb = FindResource("MenuSlideOut") as Storyboard;
            sb.Completed += SlidingOut_Completed;
            sb.Begin();
        }

        private void SlidingOut_Completed(object sender, EventArgs e)
        {
            MenuButton.IsEnabled = true;
            IsSlidingOut = false;
        }


        protected void ToggleOn()
        {
            MainContent.ColumnDefinitions.Insert(0, MenuColumnInMain);
            Grid.SetColumn(MainContent.Children[0], 1);
            Grid.SetColumn(MainContent.Children[1], 1);

            Border RightBorder = new Border();
            RightBorder.BorderThickness = new Thickness(0, 0, 2, 0);
            RightBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0099e6"));

            MenuPanel.Children.Add(RightBorder);
            Grid.SetColumn(RightBorder, 0);

            PackIcon p = new PackIcon();
            p.Kind = PackIconKind.PinOff;
            p.Height = 12;
            p.Width = 12;
            p.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Gray"));

            ToggleButton.Content = p;
            ToggleButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("White"));
            ToggleButton.Margin = new Thickness(2, 2, 4, 2);
        }

        protected void ToggleOff()
        {
            MenuPanel.Children.RemoveAt(MenuPanel.Children.Count - 1); 
            MainContent.ColumnDefinitions.Remove(MenuColumnInMain);

            PackIcon p = new PackIcon();
            p.Kind = PackIconKind.Pin;
            p.Height = 12;
            p.Width = 12;
            p.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Orange"));

            ToggleButton.Margin = new Thickness(2);
            ToggleButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("LightCyan"));
            ToggleButton.Content = p;
        }

        #endregion


        #region Other


        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure want to log out?", "Log out", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.OK)
            {
                DialogResult = true;
            }
        }

        private void ManipulationBoundaryFeedbackHandled(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }

        private void MenuPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        private void MainGridPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(IsMenuPinned == false && MenuPanel.Visibility == Visibility.Visible && IsSlidingOut == false)
            {
                MenuPanelSlideOut();
            }
            MainGridPanel.Focus();
        }


        #endregion End Other
    }
}
