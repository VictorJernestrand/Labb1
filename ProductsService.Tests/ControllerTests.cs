using ProductsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace ProductsService.Tests
{
    public class ControllerTests : IClassFixture<ProductsFixture>
    {
        ProductsFixture _fixture;
        public ControllerTests(ProductsFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public async Task GetProductById_Returns_Product()
        {
            using (var client = new TestClientProvider().Client)
            {
                var productResponse = await client.GetAsync($"/api/products/{_fixture.Product.Id}");

                using (var responseStream = await productResponse.Content.ReadAsStreamAsync())
                {
                    var product = await JsonSerializer.DeserializeAsync<Product>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    var test = product.Id;

                    Assert.Equal(_fixture.Product.Id, test);
                }
            }
        }

        [Fact]
        public async Task GetAllProducts_Returns_Ok()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/products/");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetProductById_Returns_NotFound()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/products/" + 999);
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
        [Fact]
        public async Task GetProductById_WrongDataType_Returns_BadRequest()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync($"/api/products/" + Guid.NewGuid());
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetProductById_Returns_OK()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/products/" + 1);
                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task GetProductById_Returns_ImageURLFormat()
        {
            using (var client = new TestClientProvider().Client)
            {
                var orderResponse = await client.GetAsync($"/api/products/{1}");

                using (var responseStream = await orderResponse.Content.ReadAsStreamAsync())
                {
                    var product = await JsonSerializer.DeserializeAsync<Product>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    var image = product.ImageURL;

                    Assert.IsType<string>(image);
                }
            }
        }

        [Fact]
        public async void CreateTestProduct_GetId_Returns_Ok()
        {
            using (var client = new TestClientProvider().Client)
            {
                var productId = _fixture.Product.Id;

                var response = await client.GetAsync("/api/products/" + productId);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task UpdateProductName_Return_Ok()
        {
            using (var client = new TestClientProvider().Client)
            {
                var product = _fixture.Product;

                product.Name = "Updated Test Product";

                var payload = JsonSerializer.Serialize(product);
                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

                var response = await client.PutAsync("/api/products/update/" + product.Id, content);

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var updatedProduct = await JsonSerializer.DeserializeAsync<Product>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                }
            }
        }

        [Fact]
        public async Task GetAllProducts_Returns_AllProducts()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/products");
                var productResponse = await response.Content.ReadAsStringAsync();
                var allProducts = JsonSerializer.Deserialize<IEnumerable<Product>>(productResponse);
                List<Product> actualProducts = new List<Product>();
                foreach (var product in allProducts)
                {
                    actualProducts.Add(product);
                }
                Assert.Equal(16, actualProducts.Count);
            }
        }

    }
}
