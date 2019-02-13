using NewSalesProject.Supports;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSalesProject.Model
{
    public partial class CargoFlight
    {
        public int Id { get; set; }
        public DateTime? ReturnFlight { get; set; }
        public DateTime? ComeFlight { get; set; }
        public int CheckedBaggage { get; set; }
        public int TicketPrice { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

    }
}
