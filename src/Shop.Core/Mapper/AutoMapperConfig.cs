using AutoMapper;
using Shop.Core.Domain;
using Shop.Core.DTO;
using System;

namespace Shop.Core.Mapper
{
    public static class AutoMapperConfig
    {
        public static IMapper GetMapper()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>();
                cfg.CreateMap<User, UserDto>()
                    .ForMember(m => m.Role, o => o.MapFrom(p => 
                        (RoleDto)Enum.Parse(typeof(RoleDto), p.Role.ToString(), true)));
            }).CreateMapper();
    }
}
