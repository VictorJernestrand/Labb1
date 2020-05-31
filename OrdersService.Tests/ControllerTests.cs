using OrdersService.Models;
using ProductsService.Tests;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
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
        public async void CreateTestOrder_UpdateWithWrongId__Returns_BadRequest()
        {
            var order = new Order
            {
                UserId = Guid.NewGuid().ToString(),
                OrderProducts = new List<OrderProduct> { new OrderProduct { ProductId = 8, Quantity = 3 } },
                TotalPrice = 699.00M
            };
            using (var client = new TestClientProvider().Client)
            {
                var payload = JsonSerializer.Serialize(order);
                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

                var response = await client.PutAsync("/api/orders/update/" + Guid.NewGuid(), content);

                var deletedOrder = await client.DeleteAsync($"/api/orders/{order.Id}");

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
            
        }

        [Fact]
        public async Task GetOrderById_Returns_OrderId()
        {
            using (var client = new TestClientProvider().Client)
            {
                var orderResponse = await client.GetAsync($"/api/orders/{_fixture.Order.Id}");

                using (var responseStream = await orderResponse.Content.ReadAsStreamAsync())
                {
                    var order = await JsonSerializer.DeserializeAsync<Order>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    Assert.Equal(_fixture.Order.Id, order.Id);
                }
            }
        }
        [Fact]
        public async void CreateTestOrder_Returns_OrderProduct()
        {
                var order = new Order
                {
                    UserId = Guid.NewGuid().ToString(),
                    OrderProducts = new List<OrderProduct> { new OrderProduct { ProductId = 2, Quantity = 4 } },
                    TotalPrice = 299.00M
                };
                using (var client = new TestClientProvider().Client)
                {
                    var payload = JsonSerializer.Serialize(order);
                    HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync($"/api/orders/post/", content);

                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var updatedOrder = await JsonSerializer.DeserializeAsync<Order>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    await client.DeleteAsync("/api/orders/delete/" + updatedOrder.Id);

                    var orderProduct = updatedOrder.OrderProducts.FirstOrDefault();

                    Assert.IsType<OrderProduct>(orderProduct);
                }
                await client.DeleteAsync("/api/orders/delete/" + order.Id);

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
        public async Task Delete_Order_EmptyGuid_returns_NotFound()
        {
            using (var client = new TestClientProvider().Client)
            {
                var deleteresponse = await client.DeleteAsync($"/api/orders/delete/{Guid.Empty}");

                Assert.Equal(HttpStatusCode.NotFound, deleteresponse.StatusCode);
            }
        }
        [Fact]
        public void CreateOrder_Returns_NotEmptyId()
        {
            using (var client = new TestClientProvider().Client)
            {
                var order = _fixture.Order;
                var orderId = order.Id;

                Assert.NotNull(order);
                Assert.NotEqual<Guid>(Guid.Empty, orderId);
            }
        }
        [Fact]
        public async Task GetEmptyGuid_Returns_NotFound()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync($"/api/orders/{Guid.Empty}");
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        [Fact]
        public async Task UpdateOrderProductId_Return_NewOrderProductId()
        {
            using (var client = new TestClientProvider().Client)
            {
                var order = _fixture.Order;
                var orderProduct = _fixture.OrderProduct;

                order.OrderProducts.Add(new OrderProduct { ProductId = 7, Quantity = 3 });

                var payload = JsonSerializer.Serialize(order);
                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

                var response = await client.PutAsync("/api/orders/update/" + order.Id, content);

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var updatedOrder = await JsonSerializer.DeserializeAsync<Order>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    await client.DeleteAsync("/api/orders/delete/" + updatedOrder.Id);

                    var actualProduct = updatedOrder.OrderProducts.Find(x => x.ProductId == 7);

                    Assert.Equal(7, actualProduct.ProductId);
                }
            }
        }
        [Fact]
        public async Task PostOrder_Returns_Ok()
        {
            var order = new Order
            {
                UserId = Guid.NewGuid().ToString(),
                OrderProducts = new List<OrderProduct> { new OrderProduct { ProductId = 2, Quantity = 4 } },
                TotalPrice = 299.00M
            };
            using (var client = new TestClientProvider().Client)
            {
                var payload = JsonSerializer.Serialize(order);
                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"/api/orders/post/", content);

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var updatedOrder = await JsonSerializer.DeserializeAsync<Order>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    var deleteresponse = await client.DeleteAsync($"/api/orders/{updatedOrder.Id}");
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                    
                }
                var deletedOrder = await client.DeleteAsync($"/api/orders/{order.Id}");
            }
        }
    }
}
