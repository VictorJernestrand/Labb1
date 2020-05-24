using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersService.Data;
using OrdersService.Models;

namespace OrdersService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersDbContext _context;

        public OrdersController(OrdersDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutOrder(Guid id, Order order)
        //{
        //    if (id != order.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(order).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!OrderExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Orders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<Order>> PostOrder(/*Order order*/Order order)
        //{
        //    //var orderProds = order.OrderProducts.ToList();
        //    //foreach(var prod in order.OrderProducts)
        //    //{
        //    //    _context.OrderProducts.Add(prod);
        //    //    await _context.SaveChangesAsync();
        //    //}
        //    Guid id = Guid.NewGuid();
        //    Order newOrder = new Order
        //    {
        //        Id = id,
        //        OrderProducts = order.OrderProducts,
        //        TotalPrice = order.TotalPrice,
        //        User = order.User
        //    };

        //    _context.Orders.Add(newOrder);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        //}

        public ActionResult<Order> PostOrder(Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            Guid id = Guid.NewGuid();
            
            Order newOrder = new Order()
            {
                Id = id,
                OrderProducts = order.OrderProducts,
                TotalPrice = order.TotalPrice,
                UserId = order.UserId
                
            };
            //var orderProducts = newOrder.Select(x => new OrderProduct
            //{
            //    //Product = x.Product,
            //    ProductId = x.Product.Id,
            //    Quantity = x.Quantity
            //}).ToList();

            //foreach (var product in newOrder.OrderProducts)
            //{
            //    product.ProductId = 
            //}


            _context.Orders.Add(newOrder);
            _context.SaveChanges();
            //using (_context)
            //{
            //    _context.Orders.Add(new Order()
            //    {
            //        Id = id,
            //        OrderProducts = order.OrderProducts,
            //        TotalPrice = order.TotalPrice,
            //        User = order.User
            //    });

            //    _context.SaveChanges();
            //}

            return Ok();
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }

        //private bool OrderExists(int id)
        //{
        //    return _context.Orders.Any(e => e.Id == id);
        //}
    }
}
