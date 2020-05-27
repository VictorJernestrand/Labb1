using ProductsService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsService.Tests
{
    public class Dummy
    {
        public Product Product { get; set; }

        public Product GetTestProduct()
        {
            Product product = new Product()
            {
                Name = "Test Product",
                Description = "Bla bla bla",
                ImageURL = "https://www.ledr.com/colours/white.jpg",
                Price = 100
            };
            return product;
        }
    }
}
