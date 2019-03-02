using NewSalesProject.Supports;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSalesProject.Model
{
    public partial class OrderDetail
    {
        public OrderDetail()
        {
            SaleExchangeRate = 1;
            SaleInputCurrency = "VND(₫)";
        }
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int SalePrice { get; set; }
        public int Discount { get; set; }
        public int Coupon { get; set; }
        public int SaleExchangeRate { get; set; }
        public string SaleInputCurrency { get; set; }
        public bool IsNotUseStock { get; set; } = false;

        public int? OrderID { get; set; }
        public int? ProductID { get; set; }
        public int? ReceiptDetailID { get; set; }

        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

        [ForeignKey("ReceiptDetailID")]
        public virtual ReceiptDetail ReceiptDetail { get; set; }

        [NotMapped]
        public bool IsExchangeRateEnable { get; set; } = false;
    }
}
