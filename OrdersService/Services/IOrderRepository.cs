using OrdersService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersService.Services
{
    public interface IOrderRepository
    {
        public Task<Order> GetById(Guid id);
        public Task<List<Order>> GetAll();
        public Order Create(Order order);
        public bool Delete(Guid id);
        public Order GetByIdSync(Guid id);
    }
}
