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
                //Guid.NewGuid()
                Name = "Ahlgrens bilar",
                Description = "Klassiker",
                Price = 15.00M,
                ImageURL = "https://outofhome.se/media/catalog/product/cache/30/image/600x/9df78eab33525d08d6e5fb8d27136e95/1/0/1000007_ahlgrens_bilar.jpg"
            };
            Product product2 = new Product()
            {
                Id = 2,
                Name = "Turkisk peppar",
                Description = "Superstarka",
                Price = 15.00M,
                ImageURL = "https://www.fazer.fi/globalassets/inriver/resources/e5f51e07-ee3c-4a06-a014-9e859b149dce3.png?width=620&height=9999&mode=max"
            };
            Product product3 = new Product()
            {
                Id = 3,
                Name = "Marabou chokladkaka, Schweizernöt",
                Description = "Mjölkchoklad med hasselnötter, 200 gram",
                Price = 18.00M,
                ImageURL = "https://outofhome.se/media/catalog/product/cache/30/image/600x/9df78eab33525d08d6e5fb8d27136e95/m/a/marabou-schweizern_t-200g.jpg"
            };
            Products.Add(product);
            Products.Add(product2);
            Products.Add(product3);
        }

    }
}
