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
    public class OverviewViewModel : ViewModelBase, IViewModel
    {
        public OverviewViewModel()
        {
            Name = "OverView";
            SetMenuImage(PackIconKind.ViewDashboard);
        }


    }
}
