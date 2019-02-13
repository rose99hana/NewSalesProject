using MaterialDesignThemes.Wpf;
using NewSalesProject.Interfaces;
using NewSalesProject.Supports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSalesProject.Views
{
    public class OrderViewModel : ViewModelBase, IViewModel
    {
        public OrderViewModel()
        {
            Name = "Order";
            SetMenuImage(PackIconKind.AccountStar);
        }
    }
}
