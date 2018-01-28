using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Shop.Core.DTO;

namespace Shop.Core.Services
{
    public class ServiceClient : IServiceClient
    {
        private readonly HttpClient _httpClient;

        public ServiceClient()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Remove("Accept");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync("http://localhost:3580/products");
            if (!response.IsSuccessStatusCode)
            {
                return Enumerable.Empty<ProductDto>();
            }
            var content = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(content);

            return products;
        }
    }
}
