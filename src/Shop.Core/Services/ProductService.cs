using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Shop.Core.Domain;
using Shop.Core.DTO;
using Shop.Core.Repositories;

namespace Shop.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository,
                IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public ProductDto Get(Guid id)
        {
            var product = _productRepository.Get(id);

            return product == null ? null : _mapper.Map<ProductDto>(product);
        }

        public IEnumerable<ProductDto> GetAll()
            => _productRepository
                .GetAll()
                .Select(p => _mapper.Map<ProductDto>(p));

        public void Add(Guid id, string name, string category, decimal price)
        {
            var product = new Product(id, name, category, price);
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

        public void Delete(Guid id)
            => _productRepository.Delete(id);
    }
}
