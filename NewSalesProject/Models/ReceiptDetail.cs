using NewSalesProject.Supports;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSalesProject.Model
{
    public class ReceiptDetail : ModelBase
    {
        public ReceiptDetail()
        {
            Quantity = 1;
            IsTaxIncluding = true;
            AddNewToPriceList = true;
            IsUsedToSell = false;
        }

        public int Id { get; set; }
        public int Quantity { get; set; }
        public Decimal Price { get; set; }
        public Decimal Discount { get; set; }
        public Decimal Coupon { get; set; }
        public bool IsTaxIncluding { get; set; }
        public bool IsUsedToSell { get; set; }

        public bool IsSoldOut { get; set; } = false;
        public int SoldQty { get; set; }

        public int ProductID { get; set; }
        public int GoodsReceiptID { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
        [ForeignKey("GoodsReceiptID")]
        public virtual GoodsReceipt GoodsReceipt { get; set; }


        private Decimal taxAmount;
        [NotMapped]
        public Decimal TaxAmount
        {
            get { return taxAmount; }
            set
            {
                taxAmount = value;
                OnPropertyChanged("TaxAmount");
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


        private Decimal taxIncSubTotal;
        [NotMapped]
        public Decimal TaxIncSubTotal
        {
            get { return taxIncSubTotal; }
            set
            {
                taxIncSubTotal = value;
                OnPropertyChanged("TaxIncSubTotal");
            }
        }

        private Decimal taxIncTotal;
        [NotMapped]
        public Decimal TaxIncTotal
        {
            get { return taxIncTotal; }
            set
            {
                taxIncTotal = value;
                OnPropertyChanged("TaxIncTotal");
            }
        }

        private Decimal taxIncPrice;
        [NotMapped]
        public Decimal TaxIncPrice
        {
            get { return taxIncPrice; }
            set
            {
                taxIncPrice = value;
                OnPropertyChanged("TaxIncPrice");
            }
        }

        private Decimal discountAmount;
        [NotMapped]
        public Decimal DiscountAmount
        {
            get { return discountAmount; }
            set
            {
                discountAmount = value;
                OnPropertyChanged("DiscountAmount");
            }
        }

        private Decimal couponAmount;
        [NotMapped]
        public Decimal CouponAmount
        {
            get { return couponAmount; }
            set
            {
                couponAmount = value;
                OnPropertyChanged("CouponAmount");
            }
        }

        private Decimal totalDiscount;
        [NotMapped]
        public Decimal TotalDiscount
        {
            get { return totalDiscount; }
            set
            {
                totalDiscount = value;
                OnPropertyChanged("TotalDiscount");
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

        private bool addNewToPriceList;
        [NotMapped]
        public bool AddNewToPriceList
        {
            get { return addNewToPriceList; }
            set
            {
                addNewToPriceList = value;
                OnPropertyChanged("AddNewToPriceList");
            }
        }


        //private int _VNDPrice;
        //[NotMapped]
        //public int VNDPrice
        //{
        //    get { return _VNDPrice; }
        //    set
        //    {
        //        _VNDPrice = value;
        //        OnPropertyChanged("VNDPrice");
        //    }
        //}

        //private int _VNDTaxIncPrice;
        //[NotMapped]
        //public int VNDTaxIncPrice
        //{
        //    get { return _VNDTaxIncPrice; }
        //    set
        //    {
        //        _VNDTaxIncPrice = value;
        //        OnPropertyChanged("VNDTaxIncPrice");
        //    }
        //}

    }
}
