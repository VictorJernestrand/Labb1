using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersService.Data;
using OrdersService.Models;
using OrdersService.Services;

namespace OrdersService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersDbContext _context;
        private readonly IOrderRepository _orderRepository;

        public OrdersController(OrdersDbContext context, IOrderRepository orderRepository)
        {
            _context = context;
            _orderRepository = orderRepository;
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
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(Guid id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!OrderExists(id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }
            return Ok(order);
            //return NoContent();
        }

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
        [HttpPost("Create")]
        public /*async Task<ActionResult>*/ ActionResult Create([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }
            //var createdProduct = await _productRepository.Create(product);
            var createdOrder = _orderRepository.Create(order);

            return Ok(createdOrder);
        }
        /*[HttpDelete]*//*("{id}")]*/
        [HttpDelete("delete/{id}")]
        public ActionResult<int> Delete(Guid id)
        {
            var wasDeleted = _orderRepository.Delete(id);
            if (wasDeleted)
            {
                return Ok(id);
            }
            else
            {
                return NotFound(id);
            }
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(Guid id)
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
