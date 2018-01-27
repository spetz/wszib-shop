using AutoMapper;
using Shop.Core.Domain;
using Shop.Core.DTO;
using Shop.Web.Models;
using System;

namespace Shop.Web.Framework
{
    public static class AutoMapperConfig
    {
        public static IMapper GetMapper()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cart, CartDto>();
                cfg.CreateMap<CartItem, CartItemDto>();
                cfg.CreateMap<CartDto, CartViewModel>();
                cfg.CreateMap<CartItemDto, CartItemViewModel>();
                cfg.CreateMap<Order, OrderDto>();
                cfg.CreateMap<OrderItem, OrderItemDto>();
                cfg.CreateMap<OrderDto, OrderViewModel>();
                cfg.CreateMap<OrderItemDto, OrderItemViewModel>();
                cfg.CreateMap<Product, ProductDto>();
                cfg.CreateMap<User, UserDto>()
                    .ForMember(m => m.Role, o => o.MapFrom(p => 
                        (RoleDto)Enum.Parse(typeof(RoleDto), p.Role.ToString(), true)));
            }).CreateMapper();
    }
}
