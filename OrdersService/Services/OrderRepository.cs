using Microsoft.EntityFrameworkCore;
using OrdersService.Data;
using OrdersService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersService.Services
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrdersDbContext _context;
        public OrderRepository(OrdersDbContext context)
        {
            _context = context;
        }
        public async Task<Order> GetById(Guid id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<List<Order>> GetAll()
        {
            return await _context.Orders.ToListAsync();
        }

        public Order Create(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }
        public Order GetByIdSync(Guid id)
        {
            return _context.Orders.Find(id);
        }

        public bool Delete(Guid id)
        {
            try
            {
                var order = GetByIdSync(id);
                _context.Orders.Remove(order);
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
