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
    /// Interaction logic for CategoryDetailContent.xaml
    /// </summary>
    public partial class CategoryDetailContent : UserControl
    {
        public CategoryDetailContent()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.Parent is NewView)
            {
                var uc = (NewView)this.Parent;
                Keyboard.Focus(CategoryTextBox);
            }
        }
    }
}
