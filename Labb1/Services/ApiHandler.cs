using Labb1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Labb1.Services
{
    public class ApiHandler
    {
        public HttpClient _client { get; set; }
        private const string ProductsService = "https://localhost:44381/api/";
        public const string PRODUCTS = ProductsService + "products/";
        public ApiHandler(IHttpClientFactory client)
        {
            _client = client.CreateClient();
        }
        //public List<Product> GetAllProducts()
        //{
        //    var allProducts = 
        //    return allProducts = ("https://localhost:44381/api/products/");

        //}

        public async Task<List<T>> GetAllAsync<T>(string apiPath)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, apiPath);

            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await DeserializeJson<List<T>>(response);
            }

            return null;
        }
        public async Task<T> GetOneAsync<T>(string apiPath) where T : class
        {
            var request = new HttpRequestMessage(HttpMethod.Get, apiPath);


            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await DeserializeJson<T>(response);
            }

            return null;
        }

        public async Task<T> DeserializeJson<T>(HttpResponseMessage response)
        {
            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                var post = await JsonSerializer.DeserializeAsync<T>(responseStream,
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    }
                );

                return post;
            }
        }

        public T DeserializeJson<T>(string jsonString)
        {
            var post = JsonSerializer.Deserialize<T>(jsonString,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                }
            );

            return post;
        }
    }
}
