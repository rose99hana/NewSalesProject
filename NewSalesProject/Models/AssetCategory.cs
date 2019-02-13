using NewSalesProject.Supports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSalesProject.Model
{
    public partial class AssetCategory : ModelBase
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }
    }
}
