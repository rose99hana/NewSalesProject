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
    public class GoodsReceipt : ModelBase
    {
        public GoodsReceipt()
        {
            ReceiptDetails = new HashSet<ReceiptDetail>();
            IsSoldOut = false;
            CreatedDate = DateTime.Now;
            CurrencySymbol = "¥";
            TaxRate = 8m;
        }

        public int Id { get; set; }
        public string InvoiceCode { get; set; }
        [Browsable(false)]
        public string InvoicePicture { get; set; }
        [Browsable(false)]
        public Decimal TaxRate { get; set; }
        [Browsable(false)]
        public Decimal ExchangeRate { get; set; }
        [Browsable(false)]
        public string CurrencySymbol { get; set; }
        [Browsable(false)]
        public Decimal DiscountOnReceipt { get; set; }
        [Browsable(false)]
        public Decimal ShippingFee { get; set; }
        [Browsable(false)]
        public bool IsSoldOut { get; set; }
        [Browsable(false)]
        public Decimal AdditionalFees { get; set; }
        public DateTime CreatedDate { get; set; }
        [Browsable(false)]
        public int? StoreID { get; set; }

        [Browsable(false)]
        public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; }

        //private string currencySymbol;
        //public string CurrencySymbol
        //{
        //    get { return currencySymbol; }
        //    set
        //    {
        //        currencySymbol = value;
        //        OnPropertyChanged("CurrencySymbol");
        //    }
        //}

        [Browsable(false)]
        [ForeignKey("StoreID")]
        public virtual Store Store { get; set; }


        private Decimal subTotal;
        [Browsable(false)]
        [NotMapped]
        public Decimal SubTotal
        {
            get { return subTotal; }
            set
            {
                subTotal = value;
                OnPropertyChanged("SubTotal");
            }
        }

        private Decimal totalFee;
        [Browsable(false)]
        [NotMapped]
        public Decimal TotalFee
        {
            get { return totalFee; }
            set
            {
                totalFee = value;
                OnPropertyChanged("TotalFee");
            }
        }

        //private Decimal total;
        //[NotMapped]
        //public Decimal Total
        //{
        //    get { return total; }
        //    set
        //    {
        //        total = value;
        //        OnPropertyChanged("Total");
        //    }
        //}

        public Decimal Total { get; set; }
    }
}
