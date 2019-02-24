using NewSalesProject.Supports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSalesProject.Model
{
    public class WishList : ModelBase
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<WishListDetail> WishListDetails { get; set; }
    }
}
