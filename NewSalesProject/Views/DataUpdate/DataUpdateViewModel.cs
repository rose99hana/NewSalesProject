using NewSalesProject.Enum;
using NewSalesProject.Interfaces;
using NewSalesProject.Supports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSalesProject.Views
{
    class DataUpdateViewModel : ViewModelBase, IViewModel
    {

        public DataUpdateViewModel()
        {
            Name = "Data Update";
            ViewModels.Add(new CustomerViewModel());
            ViewModels.Add(new StoreViewModel());
            ViewModels.Add(new CustomerRankViewModel());
            ViewModels.Add(new CategoryViewModel());
            ViewModels.Add(new ProductViewModel());
            ViewModels.Add(new GoodsReceiptViewModel());
            CurrentViewModel = ViewModels[0];
            CurrentViewModel.ChangeViewCommandIsEnable = false;
        }
    }
}
