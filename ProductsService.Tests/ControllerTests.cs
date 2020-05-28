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
        readonly ProductsFixture _fixture;
        private readonly Dummy _dummy;

        public ControllerTests(ProductsFixture fixture)
        {
            _fixture = fixture;
            _dummy = new Dummy();
        }
        //[Fact]
        //public async Task GetProduct()
        //{
        //    Assert.NotNull(_fixture.Product);
        //}

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
                var response = await client.GetAsync("/api/products/" + 100);
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
        public async Task GetProductById_Returns_ProductId()
        {
            using (var client = new TestClientProvider().Client)
            {
                //("{id}")]
                var productResponse = await client.GetAsync($"/api/products/{_fixture.Product.Id}");

                using (var responseStream = await productResponse.Content.ReadAsStreamAsync())
                {
                    var product = await JsonSerializer.DeserializeAsync<Product>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    Assert.Equal(_fixture.Product.Id, product.Id);
                }
            }
        }
        [Fact]
        public async Task Test()
        {
            using (var client = new TestClientProvider().Client)
            {
                var orderResponse = await client.GetAsync($"/api/products/{1}");

                using (var responseStream = await orderResponse.Content.ReadAsStreamAsync())
                {
                    var product = await JsonSerializer.DeserializeAsync<Product>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    var test = product.ImageURL;

                    Assert.IsType<string>(test);
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


        //[Fact]
        //public async Task UpdateProductName_Return_Ok2()
        //{
        //    var product = _dummy.GetTestProduct();

        //    using (var db = new TestDbProvider().ProductDbContext)
        //    {
        //        db.Add(product);
        //        await db.SaveChangesAsync();
        //    }
        //    using (var client = new TestClientProvider().Client)
        //    {
        //        product.Name = "Updated Test Product";

        //        var payload = JsonSerializer.Serialize(product);
        //        HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

        //        var response = await client.PutAsync("/api/products/update/" + product.Id, content);

        //        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //    }
        //    using (var db = new TestDbProvider().ProductDbContext)
        //    {
        //        db.Remove(product);
        //        await db.SaveChangesAsync();
        //    }
        //}

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

                    await client.DeleteAsync("/api/products/delete/" + updatedProduct.Id);

                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
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

        //[Fact]
        //public async Task GetProductById_Returns_Product()
        //{
        //    using (var client = new TestClientProvider().Client)
        //    {
        //        var allProductsResponse = await client.GetAsync("/api/products/getall");
        //        var allProducts = new List<Product>();
        //        int testId = 30;

        //        using (var responseStream = await allProductsResponse.Content.ReadAsStreamAsync())
        //        {
        //            var products = await JsonSerializer.DeserializeAsync<IEnumerable<Product>>(responseStream,
        //                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        //            allProducts = products.ToList();
        //        }
        //        if (allProducts != null && allProducts.Count > 0)
        //        {
        //            testId = allProducts[0].Id;
        //        }
        //        else
        //        {
        //            return;
        //        }

        //        var productResponse = await client.GetAsync($"/api/products/{_fixture.Product.Id}");

        //        using (var responseStream = await productResponse.Content.ReadAsStreamAsync())
        //        {
        //            var product = await JsonSerializer.DeserializeAsync<Product>(responseStream,
        //                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        //            Assert.Equal(_fixture.Product.Id, product.Id);
        //        }
        //    }

        //}

    }
}
