using Microsoft.EntityFrameworkCore;
using ProductsService.Context;
using ProductsService.Models;
using ProductsService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductsService.Tests
{
    public class RepositoryTests
    {
        //private readonly ProductApiContext _context;
        //private readonly IProductRepository _productRepository;

        //public RepositoryTests(ProductApiContext context, IProductRepository productRepository)
        //{
        //    _context = context;
        //    _productRepository = productRepository;
        //}

        //[Fact]
        //public async Task GetProductById_Returns_ProductAsync()
        //{
        //    using (var client = new TestClientProvider().Client)
        //    {

        //    Product product = await _productRepository.GetById(3);
        //    Assert.IsType<Product>(product);
        //    }
        //}

        //[Fact]
        //public void GetProductById_Returns_Correct_Id()
        //{
        //    //var productRepository = new ProductRepository();
        //    var product = _productRepository.GetById(3);

        //    Assert.Equal(3, product.Id);
        //}
    }
}
