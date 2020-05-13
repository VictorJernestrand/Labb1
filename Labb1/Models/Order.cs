using Labb1.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labb1.Models
{
    public class Order
    {
        public Order()
        {
            this.OrderDate = DateTime.Now;
            OrderProducts = new List<OrderProduct>();
        }
        public DateTime OrderDate { get; set; }
        public Guid UserId { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
