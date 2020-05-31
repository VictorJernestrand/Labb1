using Microsoft.EntityFrameworkCore;
using OrdersService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersService.Data
{
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
=> options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=OrdersService;Trusted_Connection=True;");
    }
}

