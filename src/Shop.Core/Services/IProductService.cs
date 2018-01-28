using Shop.Core.DTO;
using System;
using System.Collections.Generic;

namespace Shop.Core.Services
{
    public interface IProductService
    {
        ProductDto Get(Guid id);
        IEnumerable<ProductDto> GetAll();
        void Add(Guid id, string name, string category, decimal price);
        void Update(ProductDto product);
        void Delete(Guid id);
    }
}
