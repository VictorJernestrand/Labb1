using Labb1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labb1.ProjectData
{
    public class Dummy
    {
        public List<Product> Products { get; set; }

        public Dummy(/*List<Product> products*/)
        {
            List<Product> products = new List<Product>();
            Products = products;
            Product product = new Product()
            {
                Id = 1,
                Description = "Wobbler, mini, grön",
                Name = "Wobbler",
                Price = 59.00M,
                ImageURL = "https://contents.mediadecathlon.com/p669219/ke5d6f5a14fecb9c40ac094494f612009/669219_default.jpg?format=auto&quality=60&f=800x0"
            };
            Product product2 = new Product()
            {
                Id = 2,
                Description = "rapala Shadow, 3-pack, 11 cm, olikfärgade",
                Name = "Rapala Shadow, 3-pack",
                Price = 199.00M,
                ImageURL = "https://www.sportfiskeprylar.se/bilder/artiklar/zoom/SHADOWKIT2_1.jpg"
            };

            Product product3 = new Product()
            {
                Id = 3,
                Description = "Spinnare, 3-pack, svart, silver, koppar",
                Name = "Spinnare, 3-pack",
                Price = 79.00M,
                ImageURL = "https://bojdaspon.se/wp-content/uploads/2020/01/Abu-Droppen-Spinnare-3-Pack.jpg"
            };
            Product product4 = new Product()
            {
                Id = 4,
                Description = "Hi-Lo Wobbler, Abu Garcia, 11 cm",
                Name = "Abu Garcia Hi-Lo Wobbler",
                Price = 99.00M,
                ImageURL = "https://www.happyangler.se/i/hkg/036282948877_1/abu-garcia-hi-lo-flytande-11-cm-wobbler.jpg"
            };
            Product product5 = new Product()
            {
                Id = 5,
                Description = "Fjäderjiggar, Tungsten, 5-pack, olikfärgade",
                Name = "Fjäderjiggar 5-pack",
                Price = 129.00M,
                ImageURL = "https://cdn.shopify.com/s/files/1/1218/2078/products/Untitled_design_3_1024x1024.png?v=1508789934"
            };
            Product product6 = new Product()
            {
                Id = 6,
                Description = "Fiskelina, nylon, Stroft, 100m, 0.25 diam, 6.40kg",
                Name = "Stroft Fiskelina, nylon",
                Price = 59.00M,
                ImageURL = "https://fiskedags.se/5961-large_default/stroft-nylon-fiskelina.jpg"
            };
            Product product7 = new Product()
            {
                Id = 7,
                Description = "Fiskelina, flätad, Berkely FireLine, 110m, 0.12 diam, 10.20kg",
                Name = "FireLine, flätad",
                Price = 145.00M,
                ImageURL = "https://www.jula.se/globalassets/catalog/productimages/722130.jpg?width=458&height=458&scale=both&bgcolor=white"
            };
            Products.Add(product);
            Products.Add(product2);
            Products.Add(product3);
            Products.Add(product4);
            Products.Add(product5);
            Products.Add(product6);
            Products.Add(product7);
        }

    }
}
