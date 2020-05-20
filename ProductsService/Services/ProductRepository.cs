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
        public async Task<Product> GetById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
