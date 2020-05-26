using ProductsService.Context;
using ProductsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsService.Services
{
    public interface IProductRepository
    {
        /*Task<Product>*/ public Product GetById(int id);
        /*Task<List<Product>> */ public List<Product> GetAll();
        /*Task<Product> */public Product Create(Product product);
        public bool Delete(int id);

    }
}
