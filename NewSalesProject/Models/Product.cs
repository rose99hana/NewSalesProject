using NewSalesProject.Supports;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSalesProject.Model
{
    public partial class Product : ModelBase
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            ProductPrices = new HashSet<ProductPrice>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string OriginalName { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public int NetWeight { get; set; }
        public byte[] Picture { get; set; }

        public int? CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; }
        public virtual ICollection<ProductPrice> ProductPrices { get; set; }


        [NotMapped]
        public int StockQuantity { get; set; }

        private ProductPrice highestProductPrice;
        [NotMapped]
        public ProductPrice HighestProductPrice
        {
            get { return highestProductPrice; }
            set
            {
                highestProductPrice = value;
                OnPropertyChanged("HighestProductPrice");
            }
        }

        private ProductPrice lowestProductPrice;
        [NotMapped]
        public ProductPrice LowestProductPrice
        {
            get { return lowestProductPrice; }
            set
            {
                lowestProductPrice = value;
                OnPropertyChanged("LowestProductPrice");
            }
        }

    }
}
