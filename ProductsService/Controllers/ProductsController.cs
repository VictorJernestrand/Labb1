using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsService.Context;
using ProductsService.Filters;
using ProductsService.Models;
using ProductsService.Services;

namespace ProductsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductApiContext _context;
        private readonly IProductRepository _productRepository;

        public ProductsController(ProductApiContext context, IProductRepository productRepository)
        {
            _context = context;
            _productRepository = productRepository;
        }
        //[ApiKeyAuth]
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            return await _productRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }
        [HttpPost("Create")]
        public ActionResult Create([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            var createdProduct = _productRepository.Create(product);

            return Ok(createdProduct);
        }
        [HttpDelete("delete/{id}")]
        public ActionResult<int> Delete(int id)
        {
            var wasDeleted = _productRepository.Delete(id);
            if (wasDeleted)
            {
                return Ok(id);
            }
            else
            {
                return NotFound(id);
            }
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
