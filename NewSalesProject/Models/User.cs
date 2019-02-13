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
    public partial class User : ModelBase
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int PermissionID { get; set; }

        [ForeignKey("PermissionID")]
        [Browsable(false)]
        public virtual Permission Permission { get; set; }
    }
}
