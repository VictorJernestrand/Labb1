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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

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

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(Guid id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(order);
        }
        [HttpPost("post")]
        public ActionResult<Order> PostOrder(Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            if (order == null)
            {
                return BadRequest();
            }
            Guid id = Guid.NewGuid();
            
            Order newOrder = new Order()
            {
                Id = id,
                OrderProducts = order.OrderProducts,
                TotalPrice = order.TotalPrice,
                UserId = order.UserId
                
            };

            var createdOrder = _orderRepository.Create(newOrder);

            return Ok(createdOrder);
        }

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
    }
}
