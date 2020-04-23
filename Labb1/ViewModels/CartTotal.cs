using Labb1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labb1.ViewModels
{
    public class CartTotal
    {
        public CartTotal(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;

        }
        public Product Product { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        //public Guid CartId { get; set; }
        //public Guid ProductId { get; set; }
        //public Product Product { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
