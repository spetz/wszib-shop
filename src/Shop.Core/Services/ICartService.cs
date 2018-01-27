using Shop.Core.DTO;
using System;

namespace Shop.Core.Services
{
    public interface ICartService
    {
        CartDto Get(Guid userId);
        void AddProduct(Guid userId, Guid productId);
        void DeleteProduct(Guid userId, Guid productId);
        void Clear(Guid userId);
        void Create(Guid userId);
        void Delete(Guid userId);
    }
}
