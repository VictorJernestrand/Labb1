﻿using OrdersService.Models;
using ProductsService.Tests;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrdersService.Tests
{
    public class OrdersFixture : IDisposable
    {
        public Order Order { get; private set; }
        public OrderProduct OrderProduct { get; private set; }
        public OrdersFixture()
        {
            Order = Initialize().Result;
        }

        private async Task<Order> Initialize()
        {
            using (var client = new TestClientProvider().Client)
            {
                var payload = JsonSerializer.Serialize(
                    new Order()
                    {
                        TotalPrice = 199,
                        UserId = Guid.NewGuid().ToString(),
                        OrderProducts = new List<OrderProduct>()
                        {
                            new OrderProduct { ProductId = 5, Quantity = 1 }
                        }
                    }) ; 
                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"/api/orders/post", content);

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var createdOrder = await JsonSerializer.DeserializeAsync<Order>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    return createdOrder;
                }
            }
        }

        public async void Dispose()
        {
            using (var client = new TestClientProvider().Client)
            {
                var deletedResponse = await client.DeleteAsync("/api/orders/delete/" + Order.Id);

                using (var responseStream = await deletedResponse.Content.ReadAsStreamAsync())
                {
                    var deletedId = await JsonSerializer.DeserializeAsync<Guid>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
            }
        }
    }
}
