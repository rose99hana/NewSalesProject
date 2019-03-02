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
    public partial class Product : ModelBase
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            ProductPrices = new HashSet<ProductPrice>();
            OriginalName = "";
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string OriginalName { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public int NetWeight { get; set; }
        [Browsable(false)]
        public string Description { get; set; }
        [Browsable(false)]
        public byte[] Picture { get; set; }

        [Browsable(false)]
        public int? CategoryID { get; set; }

        [Browsable(false)]
        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }

        [Browsable(false)]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        [Browsable(false)]
        public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; }
        [Browsable(false)]
        public virtual ICollection<ProductPrice> ProductPrices { get; set; }


        [NotMapped]
        public int StockQuantity { get; set; }

        private ProductPrice highestProductPrice;
        [Browsable(false)]
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
        [Browsable(false)]
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


        protected override void CollectErrors(string propertyName)
        {
            Errors.Clear();
            switch (propertyName)
            {
                case "OriginalName":
                    CheckNotNull(propertyName, OriginalName);
                    break;
            }
        }

    }
}
