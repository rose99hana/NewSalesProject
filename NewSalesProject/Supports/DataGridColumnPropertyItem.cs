using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSalesProject.Supports
{
    public class DataGridColumnPropertyItem : NotifyUIBase
    {
        private string headerName;
        public string HeaderName
        {
            get
            {
                return headerName;
            }
            set
            {
                headerName = value;
                OnPropertyChanged("HeaderName");

            }
        }

        private bool isChecked;
        public bool IsChecked
        {
            get
            {
                return isChecked;
            }
            set
            {
                isChecked = value;
                OnPropertyChanged("IsChecked");

            }
        }
    }
}
