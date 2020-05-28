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

        //[Fact]
        //public async Task GetOrderById_Returns_OK()
        //{
        //    using (var client = new TestClientProvider().Client)
        //    {
        //        var response = await client.GetAsync("/api/orders/" + "adef8993-e4e3-4773-b054-85756882a9ad");
        //        response.EnsureSuccessStatusCode();
        //        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //    }
        //}
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
            using (var client = new TestClientProvider().Client)
            {
                var order = _fixture.Order;

                var payload = JsonSerializer.Serialize(order);
                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

                var response = await client.PutAsync("/api/orders/update/" + order.Id, content);

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var updatedOrder = await JsonSerializer.DeserializeAsync<Order>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    await client.DeleteAsync("/api/orders/delete/" + updatedOrder.Id);

                    var orderProduct = updatedOrder.OrderProducts.Find(x => x.ProductId == 5);

                    Assert.IsType<OrderProduct>(orderProduct);
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
        [Fact]
        public async Task UpdateOrderProducts_ContainingNewOrderProduct_Return_NewOrderProductId()
        {
            using (var client = new TestClientProvider().Client)
            {
                var order = _fixture.Order;
                var orderProduct = _fixture.OrderProduct;

                order.OrderProducts.Add(new OrderProduct { ProductId = 9, Quantity = 2 });

                var payload = JsonSerializer.Serialize(order);
                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

                var response = await client.PutAsync("/api/orders/update/" + order.Id, content);

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var updatedOrder = await JsonSerializer.DeserializeAsync<Order>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    //await client.DeleteAsync("/api/orders/delete/" + updatedOrder.Id);
                    List<OrderProduct> test = new List<OrderProduct>();
                    foreach (var product in order.OrderProducts)
                    {
                        test.Add(product);
                    }
                    List<OrderProduct> test2 = new List<OrderProduct>();
                    OrderProduct obj = new OrderProduct { ProductId = 9, Quantity = 2 };
                    test2.Add(obj);
                    //List<MyType> lstObj = new List<MyType> { obj2 };

                    var actualProduct = updatedOrder.OrderProducts.Find(x => x.ProductId == 7);
                    //var testar = actualProduct.ProductId;
                    //var test = updatedOrder.OrderProducts.Where(x => x.ProductId ==
                    //7).Select(x => x.ProductId == 7).FirstOrDefault();

                    //Assert.Contains(obj, test);
                }
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
                    //var testar = actualProduct.ProductId;

                    //var test = updatedOrder.OrderProducts.Where(x => x.ProductId ==
                    //7).Select(x => x.ProductId == 7).FirstOrDefault();
                    Assert.Equal(7, actualProduct.ProductId);
                }

                //using (var responseStream = await response.Content.ReadAsStreamAsync())
                //{
                //    var updatedProduct = await JsonSerializer.DeserializeAsync<Product>(responseStream,
                //        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                //}

                //Assert.Equal("Updated Test Product", product.Name);
            }
        }
    }
}
