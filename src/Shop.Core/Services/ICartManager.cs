using Shop.Core.Domain;
using System;

namespace Shop.Core.Services
{
    public interface ICartManager
    {
        Cart Get(Guid userId);
        void Set(Guid userId, Cart cart);
        void Delete(Guid userId);
    }
}
