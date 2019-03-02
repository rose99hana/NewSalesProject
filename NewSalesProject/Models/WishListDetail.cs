using NewSalesProject.Model;
using NewSalesProject.Supports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSalesProject.Model
{
    public class WishListDetail : ModelBase
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int? ProductPriceID { get; set; }

        [ForeignKey("ProductPriceID")]
        public virtual ProductPrice ProductPrice { get; set; }

        private string storeName;
        [NotMapped]
        public string StoreName
        {
            get { return storeName; }
            set
            {
                storeName = value;
                OnPropertyChanged("StoreName");
            }
        }

        private string currencySymbol;
        [NotMapped]
        public string CurrencySymbol
        {
            get { return currencySymbol; }
            set
            {
                currencySymbol = value;
                OnPropertyChanged("CurrencySymbol");
            }
        }

        private Decimal tax;
        [NotMapped]
        public Decimal Tax
        {
            get { return tax; }
            set
            {
                tax = value;
                OnPropertyChanged("Tax");
            }
        }
    }
}
