using Microsoft.EntityFrameworkCore;
using ProductsService.Context;
using ProductsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsService.Services
{
    public class ProductRepository : IProductRepository
    {
        //public ProductApiContext _context { get; set; }
        private readonly ProductApiContext _context;
        public ProductRepository(ProductApiContext context)
        {
            _context = context;
        }
        public /*async Task<Product>*/ Product GetById(int id)
        {
            //return await _context.Products.FindAsync(id);
            //return _context.Products.Find(id);
            return _context.Products.FirstOrDefault(x => x.Id == id);
        }

        public /*async Task<List<Product>>*/ List<Product> GetAll()
        {
            //return await _context.Products.ToListAsync();
            return _context.Products.ToList();
        }
        public Product Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }
        public bool Delete(int id)
        {
            try
            {
                var product = _context.Products.Find(id);
                _context.Products.Remove(product);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
