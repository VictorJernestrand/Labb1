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
        Task<Product> GetById(int id);
        Task<List<Product>> GetAll();
    }
}
