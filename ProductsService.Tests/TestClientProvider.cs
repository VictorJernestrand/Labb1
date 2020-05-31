using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductsService.Context;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ProductsService.Tests
{
    public class TestClientProvider : IDisposable
    {
        public TestServer Server { get; set; }
        public HttpClient Client { get; set; }
        public TestClientProvider()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            WebHostBuilder webHostBuilder = new WebHostBuilder();

            webHostBuilder.ConfigureServices(s => s.AddDbContext<ProductApiContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SqlDatabase"))));

            webHostBuilder.UseStartup<Startup>();

            Server = new TestServer(webHostBuilder);

            Client = Server.CreateClient();


        }
        public void Dispose()
        {
            Server?.Dispose();
            Client?.Dispose();
        }
    }
}
