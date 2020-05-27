using OrdersService.Models;
using ProductsService.Tests;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace OrdersService.Tests
{
    public class ControllerTests : IClassFixture<OrdersFixture>
    {
        OrdersFixture _fixture;
        public ControllerTests(OrdersFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public async Task GetAllOrders_Returns_Ok()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/orders/");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetNonExistingOrderById_Returns_NotFound()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/orders/" + Guid.Empty);
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetOrderById_Returns_OK()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/orders/" + "adef8993-e4e3-4773-b054-85756882a9ad");
                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
        [Fact]
        public async Task GetOrderById_Returns_Order()
        {
            using (var client = new TestClientProvider().Client)
            {
                var orderResponse = await client.GetAsync($"/api/orders/{_fixture.Order.Id}");

                using (var responseStream = await orderResponse.Content.ReadAsStreamAsync())
                {
                    var order = await JsonSerializer.DeserializeAsync<Order>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    Assert.Equal(_fixture.Order, order);
                }
            }
        }
        [Fact]
        public async Task DeleteOrder_Returns_DeletedId()
        {
            using (var client = new TestClientProvider().Client)
            {
                var orderId = _fixture.Order.Id;

                var deletedResponse = await client.DeleteAsync("/api/orders/delete/" + orderId);

                using (var responseStream = await deletedResponse.Content.ReadAsStreamAsync())
                {
                    var deletedId = await JsonSerializer.DeserializeAsync<Guid>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    Assert.Equal(orderId, deletedId);
                }
                
            }
        }
        [Fact]
        public async Task CreateOrder_Returns_NotEmptyId()
        {
            using (var client = new TestClientProvider().Client)
            {
                var order =  _fixture.Order;
                var orderId = order.Id;

                Assert.NotNull(order);
                Assert.NotEqual<Guid>(Guid.Empty, orderId);

            }
        }
    }
}
