using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersService.Models
{
    public class Order
    {
        public Order()
        {
            this.OrderDate = DateTime.Now;
        }
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public decimal TotalPrice { get; set; }
        public string UserId { get; set; }
    }
}
