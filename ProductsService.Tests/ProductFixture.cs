using ProductsService.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace ProductsService.Tests
{
    public class ProductFixture : IDisposable
    {
        public Product Product { get; private set; }
        public ProductFixture()
        {
            Product = Initialize().Result;
        }
        private async Task<Product> Initialize()
        {
            using (var client = new TestClientProvider().Client)
            {
                var payload = JsonSerializer.Serialize(
                    new Product()
                    {
                        Name = "Test product",
                        Description = "Bla bla bla",
                        Price = 99.00M,
                        ImageURL = "https://www.sportfiskeprylar.se/bilder/artiklar/zoom/SHADOWKIT2_1.jpg"
                    });
                
                HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"/api/products/create/", content);

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    
                    var createdProduct = await JsonSerializer.DeserializeAsync<Product>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                    return createdProduct;
                }
            }
        }

        public async void Dispose()
        {
            using (var client = new TestClientProvider().Client)
            {
                var deleteResponse = await client.DeleteAsync($"/api/products/delete/{Product.Id}");

                using (var responseStream = await deleteResponse.Content.ReadAsStreamAsync())
                {
                    var deletedId = await JsonSerializer.DeserializeAsync<int>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
            }
        }
    }
}
