using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Shop.Core.DTO;
using Shop.Core.Options;

namespace Shop.Core.Services
{
    public class ServiceClient : IServiceClient
    {
        private readonly HttpClient _httpClient;

        public ServiceClient(IOptions<ServiceClientOptions> options)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(options.Value.Url);
            _httpClient.DefaultRequestHeaders.Remove("Accept");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync("products");
            if (!response.IsSuccessStatusCode)
            {
                return Enumerable.Empty<ProductDto>();
            }
            var content = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(content);

            return products;
        }

        public async Task AddProductAsync(string name, string category, decimal price)
        {
            var product = new ProductDto
            {
                Name = name,
                Category = category,
                Price = price
            };
            var payload = JsonConvert.SerializeObject(product);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("products", content);
            if (response.IsSuccessStatusCode)
            {
                return;
            }
            throw new Exception($"Could not create a product. {response.ReasonPhrase}");
        }
    }
}
