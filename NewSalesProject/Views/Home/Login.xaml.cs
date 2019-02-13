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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        MainWindow m;

        public Login()
        {
            CustomSplashScreen s = new CustomSplashScreen();
            s.Show();
            m = new MainWindow();
            InitializeComponent();
            s.Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (true)
            {
                Hide();
                m.Owner = this;
                if (m.ShowDialog() == true)
                {
                    Show();
                    m = new MainWindow();
                }
                else
                {
                    m.Close();
                    Close();
                }
            }
            else
            {
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            m.Close();
            Close();
        }
    }
}
