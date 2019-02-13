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
    public partial class Customer : ModelBase
    {        
        public int Id { get; set; }
        public string Name { get; set; }

        [DisplayName("IDCard Number")]
        public string IdCardNumber { get; set; }
        public string Tel { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Relationship { get; set; }

        [Browsable(false)]
        public int? CustomerRankID { get; set; }

        [Browsable(false)]
        [ForeignKey("CustomerRankID")]
        public virtual CustomerRank CustomerRank { get; set; }

        [Browsable(false)]
        public virtual ICollection<Order> Orders { get; set; }



        protected override void CollectErrors(string propertyName)
        {
            Errors.Clear();
            switch (propertyName)
            {
                case "Name": CheckNotNull(propertyName, Name);
                    break;
            }
        }


    }
}
