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
    public partial class Store : ModelBase
    {
        public Store()
        {
            GoodsReceipts = new HashSet<GoodsReceipt>();
            ProductPrices = new HashSet<ProductPrice>();
            CurrencySymbol = "¥";
            TaxRate = 8m;

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        [Browsable(false)]
        public Decimal TaxRate { get; set; }
        [Browsable(false)]
        public string CurrencySymbol { get; set; }
        [Browsable(false)]
        public byte[] Logo { get; set; }

        [Browsable(false)]
        public virtual ICollection<GoodsReceipt> GoodsReceipts { get; set; }
        [Browsable(false)]
        public virtual ICollection<ProductPrice> ProductPrices { get; set; }

        [Browsable(false)]
        [NotMapped]
        public bool IsCheck { get; set; } = false;
        [Browsable(false)]
        [NotMapped]
        public int SpecProductQty { get; set; }

        protected override void CollectErrors(string propertyName)
        {
            Errors.Clear();
            switch (propertyName)
            {
                case "Name":
                    CheckNotNull(propertyName, Name);
                    break;
            }
        }
    }
}
