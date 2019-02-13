using NewSalesProject.Supports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSalesProject.Model
{
    public partial class CustomerRank : ModelBase
    {
        public CustomerRank()
        {
            Customers = new HashSet<Customer>();
        }

        public int Id { get; set; }
        [DisplayName("Rank")]
        public string Name { get; set; }
        public string Note { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }

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
