using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labb1.Models
{
    public class Cart
    {
        
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
