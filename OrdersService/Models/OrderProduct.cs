using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersService.Models
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        //public List<int> ProductId { get; set; }

        public int ProductId { get; set; }
        //public Product Product { get; set; }
        //public Order Order { get; set; }
        //public Guid OrderId { get; set; }

    }
}
