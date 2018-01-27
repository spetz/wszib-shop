﻿using System;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Shop.Core.Domain;
using Shop.Core.DTO;
using Shop.Core.Repositories;

namespace Shop.Core.Services
{
    public class CartService : ICartService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMemoryCache _cache;
        private readonly IMapper _mapper;

        public CartService(IUserRepository userRepository,
            IProductRepository productRepository,
            IMemoryCache cache,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _cache = cache;
            _mapper = mapper;
        }

        public CartDto Get(Guid userId)
        {
            var cart = GetCart(userId);

            return cart == null ? null : _mapper.Map<CartDto>(cart);
        }

        public void AddProduct(Guid userId, Guid productId)
        {
            var cart = GetCartOrFail(userId);
            var product = _productRepository.Get(productId);
            if (product == null)
            {
                throw new Exception($"Product with id: '{productId}' was not found.");
            }
            cart.AddProduct(product);
            SetCart(userId, cart);
        }

        public void DeleteProduct(Guid userId, Guid productId)
        {
            var cart = GetCartOrFail(userId);
            cart.DeleteProduct(productId);
            SetCart(userId, cart);
        }

        public void Clear(Guid userId)
        {
            var cart = GetCartOrFail(userId);
            cart.Clear();
            SetCart(userId, cart);
        }

        public void Create(Guid userId)
        {
            var cart = GetCart(userId);
            if (cart != null)
            {
                throw new Exception($"Cart already exists for user with id: '{userId}'.");
            }
            SetCart(userId, new Cart());
        }

        public void Delete(Guid userId)
        {
            GetCartOrFail(userId);
            _cache.Remove(GetCartKey(userId));
        }

        private Cart GetCartOrFail(Guid userId)
        {
            var cart = GetCart(userId);
            if (cart == null)
            {
                throw new Exception($"Cart was not found for user with id: '{userId}'.");
            }

            return cart;
        }

        private Cart GetCart(Guid userId) => _cache.Get<Cart>(GetCartKey(userId));

        private void SetCart(Guid userId, Cart cart) => _cache.Set(GetCartKey(userId), cart);

        private string GetCartKey(Guid userId) => $"{userId}:cart";
    }
}
