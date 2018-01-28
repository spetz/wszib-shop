using Shop.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.Core.Services
{
    public interface IServiceClient
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task AddProductAsync(string name, string category, decimal price);
    }
}
