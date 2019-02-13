using NewSalesProject.Supports;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewSalesProject.Model
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string ReservationCode { get; set; }
        public string ReservationStatus { get; set; }
        public DateTime Date { get; set; }
        public int ExchangeRate { get; set; }
        public int Discount { get; set; }
        public string DeliveryStatus { get; set; }
        public DateTime? DeliveryDate { get; set; }

        public int CustomerID { get; set; }
        public int PaymentID { get; set; }
        public int EmployeeID { get; set; }
        public int CargoFlightID { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("PaymentID")]
        public virtual Payment Payment { get; set; }
        [ForeignKey("CargoFlightID")]
        public virtual CargoFlight CargoFlight { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [NotMapped]
        public string Issued { get; set; }
        [NotMapped]
        public int StockQty { get; set; }
        [NotMapped]
        public int ExPrice { get; set; }
        [NotMapped]
        public int RealExPrice { get; set; }
        [NotMapped]
        public string StockName { get; set; }
        [NotMapped]
        public bool IsVisible { get; set; }
    }
}
