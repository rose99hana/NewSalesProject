using NewSalesProject.Supports;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSalesProject.Model
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IdentityNumber { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }

        public int PermissionID { get; set; }

        [ForeignKey("PermissionID")]
        public virtual Permission Permission { get; set; }


        public virtual ICollection<CargoFlight> CargoFlights { get; set; }
        public virtual ICollection<Order> Orders { get; set; }


    }
}
