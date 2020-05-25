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
        public async Task<Order> GetById(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<List<Order>> GetAll()
        {
            return await _context.Orders.ToListAsync();
        }
    }
}
