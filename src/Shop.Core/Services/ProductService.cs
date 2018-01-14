using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Core.Domain;
using Shop.Core.DTO;
using Shop.Core.Repositories;

namespace Shop.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ProductDto Get(Guid id)
        {
            var product = _productRepository.Get(id);

            return product == null ? null : new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category,
                Price = product.Price
            };
        }

        public IEnumerable<ProductDto> GetAll()
            => _productRepository
                .GetAll()
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Category = p.Category,
                    Price = p.Price
                });

        public void Add(string name, string category, decimal price)
        {
            var product = new Product(name, category, price);
            _productRepository.Add(product);
        }

        public void Update(ProductDto product)
        {
            var existingProduct = _productRepository.Get(product.Id);
            if (existingProduct == null)
            {
                throw new Exception($"Product was not found, id: '{product.Id}'.");
            }
            existingProduct.SetName(product.Name);
            existingProduct.SetCategory(product.Category);
            existingProduct.SetPrice(product.Price);
            _productRepository.Update(existingProduct);
        }
    }
}
