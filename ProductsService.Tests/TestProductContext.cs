//using Microsoft.EntityFrameworkCore;
//using ProductsService.Context;
//using ProductsService.Models;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Text;

//namespace ProductsService.Tests
//{
//    class TestProductContext : ProductApiContext
//    {
//        private readonly ProductApiContext _context;
//        DbContextOptions<ProductApiContext> _options;
//        public TestProductContext(ProductApiContext context, DbContextOptions<ProductApiContext> options)
//        {
//            _options = options;
//            _context = context;
//            //this.Products = new TestProductDbSet(ProductApiContext context);
//        }

//        public DbSet<Product> Products { get; set; }

//        public int SaveChanges()
//        {
//            return 0;
//        }

//        public void MarkAsModified(Product item) { }
//        public void Dispose() { }
//    {

//    }
//}
