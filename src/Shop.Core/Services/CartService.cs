using System;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Shop.Core.Domain;
using Shop.Core.DTO;
using Shop.Core.Extensions;
using Shop.Core.Repositories;

namespace Shop.Core.Services
{
    public class CartService : ICartService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICartManager _cartManager;
        private readonly IMapper _mapper;

        public CartService(IUserRepository userRepository,
            IProductRepository productRepository,
            ICartManager cartManager,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _cartManager = cartManager;
            _mapper = mapper;
        }

        public CartDto Get(Guid userId)
        {
            var cart = _cartManager.Get(userId);

            return cart == null ? null : _mapper.Map<CartDto>(cart);
        }

        public void AddProduct(Guid userId, Guid productId)
            => ExecuteOnCart(userId, cart =>
            {
                var product = _productRepository.Get(productId)
                    .FailIfNull($"Product with id: '{productId}' was not found.");
                cart.AddProduct(product);
            });

        public void DeleteProduct(Guid userId, Guid productId)
            => ExecuteOnCart(userId, cart => cart.DeleteProduct(productId));

        public void Clear(Guid userId)
            => ExecuteOnCart(userId, cart => cart.Clear());

        public void Create(Guid userId)
        {
            _cartManager.Get(userId).FailIfExists($"Cart already exists for user with id: '{userId}'.");
            _cartManager.Set(userId, new Cart());
        }

        public void Delete(Guid userId)
        {
            GetCartOrFail(userId);
            _cartManager.Delete(userId);
        }

        private void ExecuteOnCart(Guid userId, Action<Cart> action)
        {
            var cart = GetCartOrFail(userId);
            action(cart);
            _cartManager.Set(userId, cart);
        }

        private Cart GetCartOrFail(Guid userId)
            => _cartManager.Get(userId).FailIfNull($"Cart was not found for user with id: '{userId}'.");
    }
}
