using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labb1.Models
{
    public class CartItem
    {
        //public Guid Id { get; set; }
        //public Guid ProductId { get; set; }
        //public Guid CartId { get; set; }
        public int Quantity { get; set; }
        public Product Product { get; set; }
        //public System.DateTime DateCreated { get; set; }

    }
}
