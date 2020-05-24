using OrdersService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersService.Services
{
    interface IOrderRepository
    {
        Task<Order> GetById(int id);
        Task<List<Order>> GetAll();
    }
}
