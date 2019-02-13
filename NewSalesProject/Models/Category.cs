using NewSalesProject.Supports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSalesProject.Model
{
    public partial class Category : ModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }

        public virtual ICollection<Product> Products { get; set; }

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
