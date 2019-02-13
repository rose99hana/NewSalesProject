using NewSalesProject.Supports;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSalesProject.Model
{
    public partial class Payment
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime? DateOfPaymnent { get; set; }

        public int? MoneyKeeperID { get; set; }

        [ForeignKey("MoneyKeeperID")]
        public virtual MoneyKeeper MoneyKeeper { get; set; }
    }
}
