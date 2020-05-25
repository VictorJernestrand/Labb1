using ProductsService.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace ProductsService.Tests
{
    public class ControllerTests : IClassFixture<ProductFixture>
    {
        ProductFixture _fixture;
        public ControllerTests(ProductFixture fixture)
        {
            _fixture = fixture;
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
                var response = await client.GetAsync("/api/products/" + 25);
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
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
        public async Task GetProductById_Returns_Product()
        {
            using (var client = new TestClientProvider().Client)
            {
                var productResponse = await client.GetAsync($"/api/product/getbyid?id={_fixture.Product.Id}");

                using (var responseStream = await productResponse.Content.ReadAsStreamAsync())
                {
                    var product = await JsonSerializer.DeserializeAsync<Product>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    Assert.Equal(_fixture.Product.Id, product.Id);
                }
            }
        }

    }
}
