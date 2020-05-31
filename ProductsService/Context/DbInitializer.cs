using ProductsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsService.Context
{
    public static class DbInitializer
    {

        public static void Initialize(ProductApiContext context)
        {
            context.Database.EnsureCreated();

            if (context.Products.Any())
            {
                return;
            }

            var products = new Product[]
            {
                new Product
        {
            Description = "Wobbler, mini, grön",
            Name = "Wobbler",
            Price = 59.00M,
            ImageURL = "https://www.happyangler.se/i/hkg/022677210766_1/rapala-ultra-light-crank-3-cm-wobbler.jpg?$flyout$"
        },
            new Product
            {
                Description = "rapala Shadow Rap, 3-pack, 11 cm, olikfärgade",
                Name = "Rapala Shadow Rap, 3-pack",
                Price = 279.00M,
                ImageURL = "https://www.sportfiskeprylar.se/bilder/artiklar/SHADOWRAP11BUNDLE1.jpg"
            },

            new Product
            {
                Description = "Spinnare, 3-pack, svart, silver, koppar",
                Name = "Spinnare, 3-pack",
                Price = 79.00M,
                ImageURL = "https://bojdaspon.se/wp-content/uploads/2020/01/Abu-Droppen-Spinnare-3-Pack.jpg"
            },
            new Product
            {
                Description = "Hi-Lo Wobbler, Abu Garcia, 11 cm",
                Name = "Abu Garcia Hi-Lo Wobbler",
                Price = 99.00M,
                ImageURL = "https://www.happyangler.se/i/hkg/036282948877_1/abu-garcia-hi-lo-flytande-11-cm-wobbler.jpg"
            },
            new Product
            {
                Description = "Fjäderjiggar, Tungsten, 5-pack, olikfärgade",
                Name = "Fjäderjiggar 5-pack",
                Price = 129.00M,
                ImageURL = "https://cdn.shopify.com/s/files/1/1218/2078/products/Untitled_design_3_1024x1024.png?v=1508789934"
            },
                        new Product
            {
                Description = "Abu Garcia Spinnare, 12g, Reflex, Röd",
                Name = "Abu Garcia Spinnare",
                Price = 59.00M,
                ImageURL = "https://www.happyangler.se/i/hkg/036282342743_1/abu-reflex-red-12-g-spinnare.jpg?$flyout$"
            },
            new Product
            {
                Description = "Skeddrag, Toby, Abu Garcia 3-pack, olikfärgade",
                Name = "Toby Skeddrag 3-pack",
                Price = 59.00M,
                ImageURL = "https://www.happyangler.se/i/hkg/036282852020_1/abu-garcia-toby-3-pack-skeddragset.jpg?$flyout$"
            },
            new Product
            {
                Description = "Fiskelåda med 3 uppfällbara sorteringsfack, snäpplås, uppfällbart handtag",
                Name = "Fiskelåda",
                Price = 199.00M,
                ImageURL = "https://www.jula.se/globalassets/catalog/productimages/770028b.jpg?width=458&height=458&scale=both&bgcolor=white"
            },
            new Product
            {
                Description = "Fiskelina, nylon, Stroft, 100m, 0.25 diam, 6.40kg",
                Name = "Stroft Fiskelina, nylon",
                Price = 59.00M,
                ImageURL = "https://fiskedags.se/5961-large_default/stroft-nylon-fiskelina.jpg"
            },
            new Product
            {
                Description = "Fiskelina, flätad, Berkely FireLine, 110m, 0.12 diam, 10.20kg",
                Name = "FireLine, flätad",
                Price = 145.00M,
                ImageURL = "https://www.jula.se/globalassets/catalog/productimages/722130.jpg?width=458&height=458&scale=both&bgcolor=white"
            },
            new Product
            {
                Description = "Fiskespö, teleskop, 1.8-3.6m, kolfiber",
                Name = "Leo GT400, teleskopspö",
                Price = 549.00M,
                ImageURL = "https://imgaz2.staticbg.com/thumb/large/oaupload/banggood/images/5A/7A/07658788-500d-4359-923a-ba88e4864376.jpg"
            },
            new Product
            {
                Description = "Pimpelspö, mini, ultralätt",
                Name = "Pimpelspö, mini",
                Price = 199.00M,
                ImageURL = "https://i5.walmartimages.com/asr/8605a207-3410-44a1-93d9-b4eef11b9682_1.2cc67ccd8c20b9016c1e9a5c91d4bea7.jpeg"
            },
            new Product
            {
                Description = "Fiskehåv, 54x28cm",
                Name = "Fiskehåv",
                Price = 99.00M,
                ImageURL = "https://shop.kvibergs.se/wp-content/uploads/2017/11/1519.jpg"
            },
            new Product
            {
                Description = "Fiskevåg, Berkley, digital, max 23kg",
                Name = "Berkley Fiskevåg",
                Price = 249.00M,
                ImageURL = "https://www.jula.se/globalassets/catalog/productimages/771026.jpg?width=458&height=458&scale=both&bgcolor=white"
            },
            new Product
            {
                Description = "Fiskekniv, Rapala, 4\"",
                Name = "Rapala Fiskekniv",
                Price = 249.00M,
                ImageURL = "https://www.batofiske.se/pub_images/large/02646_1013.jpg"
            }
            };

            foreach (Product p in products)
            {
                context.Products.Add(p);
            }
            context.SaveChanges();


        }
    }
}
