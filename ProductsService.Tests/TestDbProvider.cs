using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductsService.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductsService.Tests
{
    
    class TestDbProvider
    {
        public ProductApiContext ProductDbContext { get; private set; }

        public TestDbProvider()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings/json")
                .Build();

            var dbOption = new DbContextOptionsBuilder<ProductApiContext>()
                .UseSqlServer(config.GetConnectionString("SqlDatabase")).Options;

            var optionsBuilder = new DbContextOptionsBuilder<ProductApiContext>();

            var context = new ProductApiContext(dbOption);

            ProductDbContext = context;
        }

        public void Dispose()
        {
            ProductDbContext?.Dispose();
        }

    }
}
