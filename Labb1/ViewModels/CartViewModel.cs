using Labb1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labb1.ViewModels
{
    public class CartViewModel
    {
        //public Guid Id { get; set; }
        public List<(int amount, Product product)> Products { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
