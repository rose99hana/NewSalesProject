using NewSalesProject.Supports;
using System;
using System.Collections.Generic;
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
        public string InvoicePicture { get; set; }
        public Decimal TaxRate { get; set; }
        public Decimal ExchangeRate { get; set; }
        public string CurrencySymbol { get; set; }
        public Decimal DiscountOnReceipt { get; set; }
        public Decimal ShippingFee { get; set; }
        public bool IsSoldOut { get; set; }
        public Decimal AdditionalFees { get; set; }
        public DateTime CreatedDate { get; set; }
        public int StoreID { get; set; }

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


        [ForeignKey("StoreID")]
        public virtual Store Store { get; set; }


        private Decimal subTotal;
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

        private Decimal total;
        [NotMapped]
        public Decimal Total
        {
            get { return total; }
            set
            {
                total = value;
                OnPropertyChanged("Total");
            }
        }


        private Decimal totalFee;
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
    }
}
