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
        

        public ProductApiContext(DbContextOptions<ProductApiContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
=> options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ProductsService;Trusted_Connection=True;");
    }


}
