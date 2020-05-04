using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labb1.Models
{
    public class OrderProduct
    {
        //public Guid Id { get; set; }
        public int Quantity { get; set; }
        //public decimal Price { get; set; }
        public Product Product { get; set; }


    }
}
