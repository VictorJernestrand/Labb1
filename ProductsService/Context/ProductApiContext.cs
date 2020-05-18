using Microsoft.EntityFrameworkCore;
using ProductsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsService.Context
{
    public class ProductApiContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductApiContext(DbContextOptions<ProductApiContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().HasData(
        new Product()
        {
            Id = 1,
            Description = "Wobbler, mini, grön",
            Name = "Wobbler",
            Price = 59.00M,
            ImageURL = "https://www.happyangler.se/i/hkg/022677210766_1/rapala-ultra-light-crank-3-cm-wobbler.jpg?$flyout$"
        },
            new Product()
            {
                Id = 2,
                Description = "rapala Shadow, 3-pack, 11 cm, olikfärgade",
                Name = "Rapala Shadow, 3-pack",
                Price = 199.00M,
                ImageURL = "https://www.sportfiskeprylar.se/bilder/artiklar/zoom/SHADOWKIT2_1.jpg"
            },

            new Product()
            {
                Id = 3,
                Description = "Spinnare, 3-pack, svart, silver, koppar",
                Name = "Spinnare, 3-pack",
                Price = 79.00M,
                ImageURL = "https://bojdaspon.se/wp-content/uploads/2020/01/Abu-Droppen-Spinnare-3-Pack.jpg"
            },
            new Product()
            {
                Id = 4,
                Description = "Hi-Lo Wobbler, Abu Garcia, 11 cm",
                Name = "Abu Garcia Hi-Lo Wobbler",
                Price = 99.00M,
                ImageURL = "https://www.happyangler.se/i/hkg/036282948877_1/abu-garcia-hi-lo-flytande-11-cm-wobbler.jpg"
            },
            new Product()
            {
                Id = 5,
                Description = "Fjäderjiggar, Tungsten, 5-pack, olikfärgade",
                Name = "Fjäderjiggar 5-pack",
                Price = 129.00M,
                ImageURL = "https://cdn.shopify.com/s/files/1/1218/2078/products/Untitled_design_3_1024x1024.png?v=1508789934"
            },
            new Product()
            {
                Id = 6,
                Description = "Fiskelina, nylon, Stroft, 100m, 0.25 diam, 6.40kg",
                Name = "Stroft Fiskelina, nylon",
                Price = 59.00M,
                ImageURL = "https://fiskedags.se/5961-large_default/stroft-nylon-fiskelina.jpg"
            },
            new Product()
            {
                Id = 7,
                Description = "Fiskelina, flätad, Berkely FireLine, 110m, 0.12 diam, 10.20kg",
                Name = "FireLine, flätad",
                Price = 145.00M,
                ImageURL = "https://www.jula.se/globalassets/catalog/productimages/722130.jpg?width=458&height=458&scale=both&bgcolor=white"
            },
            new Product()
            {
                Id = 8,
                Description = "Fiskespö, teleskop, 1.8-3.6m, kolfiber",
                Name = "Leo GT400, teleskopspö",
                Price = 549.00M,
                ImageURL = "https://imgaz2.staticbg.com/thumb/large/oaupload/banggood/images/5A/7A/07658788-500d-4359-923a-ba88e4864376.jpg"
            },
            new Product()
            {
                Id = 9,
                Description = "Pimpelspö, mini, ultralätt",
                Name = "Pimpelspö, mini",
                Price = 199.00M,
                ImageURL = "https://i5.walmartimages.com/asr/8605a207-3410-44a1-93d9-b4eef11b9682_1.2cc67ccd8c20b9016c1e9a5c91d4bea7.jpeg"
            },
            new Product()
            {
                Id = 10,
                Description = "Fiskehåv, 54x28cm",
                Name = "Fiskehåv",
                Price = 99.00M,
                ImageURL = "https://shop.kvibergs.se/wp-content/uploads/2017/11/1519.jpg"
            },
            new Product()
            {
                Id = 11,
                Description = "Fiskevåg, Berkley, digital, max 23kg",
                Name = "Berkley Fiskevåg",
                Price = 249.00M,
                ImageURL = "https://www.jula.se/globalassets/catalog/productimages/771026.jpg?width=458&height=458&scale=both&bgcolor=white"
            },
            new Product()
            {
                Id = 12,
                Description = "Fiskekniv, Rapala, 4\"",
                Name = "Rapala Fiskekniv",
                Price = 249.00M,
                ImageURL = "https://www.batofiske.se/pub_images/large/02646_1013.jpg"
            },
            new Product()
            {
                Id = 13,
                Description = "Abu Garcia Spinnare, 12g, Reflex, Röd",
                Name = "Abu Garcia Spinnare",
                Price = 59.00M,
                ImageURL = "https://www.happyangler.se/i/hkg/036282342743_1/abu-reflex-red-12-g-spinnare.jpg?$flyout$"
            },
            new Product()
            {
                Id = 14,
                Description = "Skeddrag, Toby, Abu Garcia 3-pack, olikfärgade",
                Name = "Toby Skeddrag 3-pack",
                Price = 59.00M,
                ImageURL = "https://www.happyangler.se/i/hkg/036282852020_1/abu-garcia-toby-3-pack-skeddragset.jpg?$flyout$"
            },
            new Product()
            {
                Id = 15,
                Description = "Fiskelåda med 3 uppfällbara sorteringsfack, snäpplås, uppfällbart handtag",
                Name = "Fiskelåda",
                Price = 199.00M,
                ImageURL = "https://www.jula.se/globalassets/catalog/productimages/770028b.jpg?width=458&height=458&scale=both&bgcolor=white"
            }
        );
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
=> options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ProductsService;Trusted_Connection=True;");
    }


}
