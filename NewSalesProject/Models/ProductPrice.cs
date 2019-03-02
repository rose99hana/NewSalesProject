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
    public partial class ProductPrice : ModelBase
    {
        public ProductPrice()
        {
            CurrencySymbol = "¥";
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public int? ProductID { get; set; }
        public int? StoreID { get; set; }
        public Decimal Price { get; set; }
        public Decimal Discount { get; set; }
        public Decimal Coupon { get; set; }
        public Decimal Point { get; set; }
        public string Note { get; set; }
        public string CurrencySymbol { get; set; }
        public bool IsTaxIncluding { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

        [ForeignKey("StoreID")]
        public virtual Store Store { get; set; }

        private Decimal realPrice;
        [NotMapped]
        public Decimal RealPrice
        {
            get { return realPrice; }
            set
            {
                realPrice = value;
                OnPropertyChanged("RealPrice");
            }
        }


        private Decimal exchangeRealPrice;
        [NotMapped]
        public Decimal ExchangeRealPrice
        {
            get { return exchangeRealPrice; }
            set
            {
                exchangeRealPrice = value;
                OnPropertyChanged("ExchangeRealPrice");
            }
        }

        private Decimal exchangePrice;
        [NotMapped]
        public Decimal ExchangePrice
        {
            get { return exchangePrice; }
            set
            {
                exchangePrice = value;
                OnPropertyChanged("ExchangePrice");
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

        

        //private bool isCaculated = false;
        //[NotMapped]
        //public bool IsCaculated
        //{
        //    get { return isCaculated; }
        //    set
        //    {
        //        isCaculated = value;
        //        OnPropertyChanged("IsCaculated");
        //    }
        //}
    }
}
