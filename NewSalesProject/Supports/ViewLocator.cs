using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewSalesProject.Views;

namespace NewSalesProject.Supports
{
    public class ViewLocator : NotifyUIBase
    {
        public ViewLocator()
        {

        }

        private ProductView productView = new ProductView();
        public ProductView ProductView
        {
            get
            {
                return productView;
            }
            set
            {
                productView = value;
                OnPropertyChanged("ProductView");
            }
        }

        private CustomerView customerView = new CustomerView();
        public CustomerView CustomerView
        {
            get
            {
                return customerView;
            }
            set
            {
                customerView = value;
                OnPropertyChanged("CustomerView");
            }
        }
    }
}
